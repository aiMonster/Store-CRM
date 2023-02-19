using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Interfaces;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("leavings/")]
    [ApiController]
    public class LeavingsController : ControllerBase
    {
        private readonly ILeavingsService _leavingsService;

        public LeavingsController(ILeavingsService leavingsService)
        {
            _leavingsService = leavingsService;
        }

        // <summary>
        /// Get leavings data for all products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">An array of leavings data</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeavingDTO>>> GetAll()
        {
            return Ok(await _leavingsService.GetAllAsync());
        }
    }
}

