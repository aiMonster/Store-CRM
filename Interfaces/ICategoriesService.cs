using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface ICategoriesService
	{
        Task<int> AddNewCategoryAsync(CreateCategoryDTO category);
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task RemoveCategoryByIdAsync(int id);
    }
}

