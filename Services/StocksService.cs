using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Enums;
using StoreCRM.Helpers;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
    public class StocksService : IStocksService
    {
        private readonly IMapper _mapper;
        private readonly UserResolver _userResolver;
        private readonly StoreCrmDbContext _dbContext;

        public StocksService(IMapper mapper, UserResolver userResolver, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _userResolver = userResolver;
            _dbContext = dbContext;
        }

        public async Task<List<StockDTO>> GetAllAsync(bool includeRemoved)
        {
            var query = _dbContext.Stocks.AsQueryable();

            query = includeRemoved ? query : query.Where(stock => !stock.IsRemoved);

            var stocks = await query.ToListAsync();

            return _mapper.Map<List<StockDTO>>(stocks);
        }

        public async Task<int> AddStockAsync(CreateStockDTO stock)
        {
            var stockEntity = _mapper.Map<Stock>(stock);

            await _dbContext.Stocks.AddAsync(stockEntity);
            await _dbContext.SaveChangesAsync();

            return stockEntity.Id;
        }

        public async Task RemoveStockByIdAsync(int id)
        {
            var stock = await _dbContext.Stocks.FindAsync(id);

            if (stock == null || stock.IsRemoved)
            {
                throw new Exception("Stock doesn't exist");
            }

            stock.IsRemoved = true;

            _dbContext.Update(stock);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AddProductsAsync(int stockId, CreatePostingDTO postingInfo)
        {
            var stock = await _dbContext.Stocks.FindAsync(stockId);

            if (stock == null || stock.IsRemoved)
            {
                throw new Exception("Stock doesn't exist");
            }

            var agent = await _dbContext.Agents.FindAsync(postingInfo.AgentId);

            if (agent == null)
            {
                throw new Exception("Agent doesn't exist");
            }

            var productsIds = postingInfo.Products.Select(item => item.ProductId);
            var foundProductsCount = await _dbContext.Products.Where(product => productsIds.Contains(product.Id)).CountAsync();
            var allProductsExist = foundProductsCount == productsIds.Count();

            if (!allProductsExist)
            {
                throw new Exception("Some of the products doesn't exist");
            }

            var posting = new Posting()
            {
                Date = DateTimeOffset.UtcNow,
                StockId = stockId,
                AgentId = postingInfo.AgentId,
                AddedById = _userResolver.GetUserId()
            };

            await _dbContext.Postings.AddAsync(posting);
            await _dbContext.SaveChangesAsync();

            var postingProducts = postingInfo.Products.Select(item => {
                var postingProduct = _mapper.Map<PostingProduct>(item);
                postingProduct.PostingId = posting.Id;

                return postingProduct;
            });

            await _dbContext.PostingProducts.AddRangeAsync(postingProducts);
            await _dbContext.SaveChangesAsync();

            return posting.Id;
        }

        public async Task<List<PostingDTO>> GetAllPostingsAsync()
        {
            var postingsEntities = await _dbContext.Postings
                .Include(posting => posting.AddedBy)
                .Include(posting => posting.Stock)
                .Include(posting => posting.Agent)
                .Include(posting => posting.Products)
                .ToListAsync();

            var postings = _mapper.Map<List<PostingDTO>>(postingsEntities);

            postings.ForEach(posting =>
            {
                posting.Totals = new List<PriceDTO>();

                postingsEntities.First(entity => entity.Id == posting.Id).Products
                    .GroupBy(posting => posting.Currency)
                    .ToList()
                    .ForEach(products =>
                    {
                        posting.Totals.Add(new PriceDTO
                        {
                            Currency = products.Key,
                            Value = products.Sum(product => product.Count * product.PricePerItem)
                        });
                    });
            });

            return postings;
        }

        public async Task<List<PostingItemDTO>> GetAllPostingProductsAsync(int postingId)
        {
            var items = await _dbContext.PostingProducts.Where(product => product.PostingId == postingId)
                .Include(item => item.Product).ThenInclude(product => product.AddedBy)
                .Include(item => item.Product).ThenInclude(product => product.Category)
                .Include(item => item.Product).ThenInclude(product => product.Attachments)
                .ToListAsync();

            return _mapper.Map<List<PostingItemDTO>>(items);
        }
    }
}

