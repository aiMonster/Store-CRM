using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
	public class ProductsService : IProductsService
	{
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;

        public ProductsService(IMapper mapper, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
           
            return _mapper.Map<List<ProductDTO>>(products);
        }

    }
}

