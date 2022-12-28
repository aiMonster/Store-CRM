using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StoreCRM.Interfaces;
using StoreCRM.DTOs;
using StoreCRM.Services;
using StoreCRM.Entities;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("products/")]
    [ApiController]
    public class ProductsController : ControllerBase
	{
		private readonly IProductsService _productsService;

		public ProductsController(IProductsService productsService)
		{
			_productsService = productsService;
		}

        /// <summary>
		/// Get all products
		/// </summary>
		/// <returns></returns>
		/// <response code="200">An array of products</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            return Ok(await _productsService.GetAllAsync());
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Added product id</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPost]
        public async Task<ActionResult> AddNewProduct([FromBody] CreateProductDTO product)
        {
            try
            {
                return Ok(await _productsService.AddProductAsync(product));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove product
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Product doesn't exists</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProduct([FromRoute]int id)
        {
            try
            {
                await _productsService.RemoveProductAsync(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

