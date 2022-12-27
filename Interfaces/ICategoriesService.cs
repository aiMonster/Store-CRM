using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface ICategoriesService
	{
        Task<Guid> AddNewCategoryAsync(CreateCategoryDTO category);
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task RemoveCategoryByIdAsync(Guid id);
    }
}

