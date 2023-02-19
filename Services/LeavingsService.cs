using System;
using AutoMapper;
using StoreCRM.Context;
using StoreCRM.Helpers;
using StoreCRM.Interfaces;
using StoreCRM.DTOs;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Enums;

namespace StoreCRM.Services
{
	public class LeavingsService : ILeavingsService
    {
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;

        public LeavingsService(IMapper mapper, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<LeavingDTO>> GetAllAsync()
        {
            var products = await _dbContext.Products
                .Include(product => product.Postings)
                .ThenInclude(postingProduct => postingProduct.Posting)
                .ToListAsync();

            var leavings = products.Select((product) =>
            {
                DateTimeOffset? lastPostingDate = product.Postings.Count() > 0 ? product.Postings
                    .Select(postingProduct => postingProduct.Posting.Date)
                    .OrderBy((date) => date)
                    .LastOrDefault() : null;

                var totalPostingsProductCount = product.Postings
                    .Sum((postingProduct) => postingProduct.Count);

                var totalCost = product.Postings
                    .Select((postingProduct) =>
                    {
                        var price = ConvertToDefaultCurrency(postingProduct.PricePerItem, postingProduct.Currency);
                        return price * postingProduct.Count;
                    })
                    .Sum();

                var costPerItem = totalPostingsProductCount > 0 ? new PriceDTO
                {
                    Value = totalCost / totalPostingsProductCount,
                    Currency = Constants.DEFAULT_CURRENCY

                } : null;

                return new LeavingDTO
                {
                    Product = _mapper.Map<ProductDTO>(product),
                    LastPostingDate = lastPostingDate,
                    InStockCount = totalPostingsProductCount,
                    Cost = costPerItem
                };
            });

            return leavings.ToList();
        }

        // TODO: Make as extension and use some currencies service
        private decimal ConvertToDefaultCurrency(decimal value, Currency currency)
        {
            if (currency == Constants.DEFAULT_CURRENCY)
            {
                return value;
            }

            if (currency == Currency.USD)
            {
                return value * 40;
            }

            throw new Exception("Currency is not supported");
        }
        
    }
}

