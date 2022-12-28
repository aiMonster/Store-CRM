using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Helpers;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
	public class ProductsService : IProductsService
	{
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;
        private readonly UserResolver _userResolver;
        private readonly IAttachmentsService _attachmentsService;

        public ProductsService(
            IMapper mapper,
            StoreCrmDbContext dbContext,
            UserResolver userResolver,
            IAttachmentsService attachmentsService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userResolver = userResolver;
            _attachmentsService = attachmentsService;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _dbContext.Products
                .Include(product => product.AddedBy)
                .Include(product => product.Category)
                .Include(product => product.Attachments)
                .ToListAsync();
           
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<int> AddProductAsync(CreateProductDTO product)
        {
            var category = await _dbContext.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var attachments = await _dbContext.Attachments
                .Where(attachment => product.Attachments.Contains(attachment.Id))
                .ToListAsync();

            if (attachments.Count != product.Attachments.Count)
            {
                throw new Exception("Some of the attachments could not be found");
            }

            var productEntity = _mapper.Map<Product>(product);

            productEntity.AddedById = _userResolver.GetUserId();
            
            await _dbContext.Products.AddAsync(productEntity);
            await _dbContext.SaveChangesAsync();

            attachments.ForEach(attachment => attachment.ProductId = productEntity.Id);

            _dbContext.Attachments.UpdateRange(attachments);
            await _dbContext.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task RemoveProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                throw new Exception("Product doesn't exist");
            }

            var attachmentsIds = await _dbContext.Attachments
                .Where(attachment => attachment.ProductId == id)
                .Select(attachment => attachment.Id)
                .ToListAsync();

            await _attachmentsService.RemoveRangeAsync(attachmentsIds);

            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

