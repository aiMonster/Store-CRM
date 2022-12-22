using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface IProductsService
	{
        Task<List<ProductDTO>> GetAllAsync();
    }
}
