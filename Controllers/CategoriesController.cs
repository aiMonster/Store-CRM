using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Interfaces;
using StoreCRM.Services;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("categories/")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Created category id</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPost]
        public async Task<ActionResult> AddNewCategory([FromBody] CreateCategoryDTO category)
        {
            try
            {
                return Ok(await _categoriesService.AddNewCategoryAsync(category));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of categories</response>
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _categoriesService.GetAllCategoriesAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove category with all subcategories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCategoryById([FromRoute]Guid id)
        {
            try
            {
                await _categoriesService.RemoveCategoryByIdAsync(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

