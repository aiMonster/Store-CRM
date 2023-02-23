using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface IProductsService
	{
        Task<List<ProductDTO>> GetAllAsync(bool includeRemoved);
        Task<int> AddProductAsync(CreateProductDTO product);
        Task RemoveProductAsync(int id);
    }
}
