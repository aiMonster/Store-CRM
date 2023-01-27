using System;
using StoreCRM.DTOs;
using StoreCRM.Enums;

namespace StoreCRM.Interfaces
{
    public interface IStocksService
    {
        Task<List<StockDTO>> GetAllAsync();
        Task<int> AddStockAsync(CreateStockDTO stock);
        Task RemoveStockByIdAsync(int id);
    }
}

