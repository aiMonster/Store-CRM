using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StoreCRM.Interfaces;
using StoreCRM.DTOs;

namespace StoreCRM.Controllers
{
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            return Ok(await _productsService.GetAllAsync());
        }
    }
}

