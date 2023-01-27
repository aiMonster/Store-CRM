using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Enums;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
    public class StocksService : IStocksService
    {
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;

        public StocksService(IMapper mapper, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<StockDTO>> GetAllAsync()
        {
            var stcks = await _dbContext.Stocks.ToListAsync();

            return _mapper.Map<List<StockDTO>>(stcks);
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

            if (stock == null)
            {
                throw new Exception("Stock doesn't exist");
            }

            _dbContext.Remove(stock);
            await _dbContext.SaveChangesAsync();
        }
    }
}

