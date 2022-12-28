using System;
using System.Net.Mail;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
	public class CategoriesService : ICategoriesService
    {
        private readonly StoreCrmDbContext _dbContext;

        public CategoriesService(StoreCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddNewCategoryAsync(CreateCategoryDTO category)
        {
            if (category.ParentId != null)
            {
               var parentCategory = await _dbContext.Categories.FindAsync(category.ParentId);

                if (parentCategory == null)
                {
                    throw new Exception("Parent category doesn't exist");
                }
            }

            var newCategory = new Category()
            {
                ParentId = category.ParentId,
                Name = category.Name
            };

            await _dbContext.Categories.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();

            return newCategory.Id;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return MapCategoryIncludingChildren(null, categories);
        }

        public async Task RemoveCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Category doesn't exist");
            }

            var categories = await _dbContext.Categories.ToListAsync();

            var touchedChildren = GetTouchedCategories(id, categories);

            var touchedCategories = new List<Category>();

            touchedCategories.Add(category);
            touchedCategories.AddRange(touchedChildren);

            var touchedCategoriesIds = touchedCategories.Select(category => category.Id);
            var productWithRemovingCategoryExists = await _dbContext.Products
                .AnyAsync(product => touchedCategoriesIds.Contains(product.CategoryId));

            if (productWithRemovingCategoryExists)
            {
                throw new Exception("Category or any of its subcategories has products");
            }

            _dbContext.RemoveRange(touchedCategories);
            await _dbContext.SaveChangesAsync();
        }

        private List<Category> GetTouchedCategories(int parentId, List<Category> categories)
        {
            var touchedCategories = new List<Category>();

            categories.Where(category => category.ParentId == parentId).ToList().ForEach((category) =>
            {
                touchedCategories.Add(category);
                touchedCategories.AddRange(GetTouchedCategories(category.Id, categories));
            });

            return touchedCategories;
        }

        private List<CategoryDTO> MapCategoryIncludingChildren(int? parentId, List<Category> categories)
        {
            return categories.Where(category => category.ParentId == parentId).Select(category =>
            {
                return new CategoryDTO()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = parentId,
                    Children = MapCategoryIncludingChildren(category.Id, categories)
                };
            }).ToList();
        }
    }
}

