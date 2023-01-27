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
        Task<int> AddProductsAsync(int id, List<PostingNewItemDTO> products);
        Task<List<PostingDTO>> GetAllPostingsAsync();
        Task<List<PostingItemDTO>> GetAllPostingProductsAsync(int postingId);
    }
}

