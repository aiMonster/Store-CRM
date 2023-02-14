using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Interfaces;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("agents/")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentsService _agentsService;

        public AgentsController(IAgentsService agentsService)
        {
            _agentsService = agentsService;
        }

        // <summary>
        /// Get all agents
        /// </summary>
        /// <returns></returns>
        /// <response code="200">An array of agents</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDTO>>> GetAll()
        {
            return Ok(await _agentsService.GetAllAgentsAsync());
        }

        /// <summary>
        /// Add new agent
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Added agent id</response>
        [HttpPost]
        public async Task<ActionResult> AddNewAgent([FromBody] CreateAgentDTO agent)
        {
            return Ok(await _agentsService.AddNewAgentAsync(agent));
        }
    }
}

