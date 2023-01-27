using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Enums;
using StoreCRM.Interfaces;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("stocks/")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStocksService _stocksService;

        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        // <summary>
        /// Get all stocks
        /// </summary>
        /// <returns></returns>
        /// <response code="200">An array of stocks</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDTO>>> GetAll()
        {
            return Ok(await _stocksService.GetAllAsync());
        }

        /// <summary>
        /// Add new stock
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Added stock id</response>
        [HttpPost]
        public async Task<ActionResult> AddNewStock([FromBody] CreateStockDTO stock)
        {
            return Ok(await _stocksService.AddStockAsync(stock));
        }

        /// <summary>
        /// Remove stock
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveStockById([FromRoute] int id)
        {
            try
            {
                await _stocksService.RemoveStockByIdAsync(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

