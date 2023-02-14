using System;
using StoreCRM.DTOs;
using StoreCRM.Enums;

namespace StoreCRM.Interfaces
{
    public interface IStocksService
    {
        Task<List<StockDTO>> GetAllAsync(bool includeRemoved);
        Task<int> AddStockAsync(CreateStockDTO stock);
        Task RemoveStockByIdAsync(int id);
        Task<int> AddProductsAsync(int id, CreatePostingDTO postingInfo);
        Task<List<PostingDTO>> GetAllPostingsAsync();
        Task<List<PostingItemDTO>> GetAllPostingProductsAsync(int postingId);
    }
}

