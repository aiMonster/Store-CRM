using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
	public class AgentsService: IAgentsService
    {
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;

        public AgentsService(IMapper mapper, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<int> AddNewAgentAsync(CreateAgentDTO agent)
        {
            var agentEntity = _mapper.Map<Agent>(agent);
            agentEntity.AddedOn = DateTime.UtcNow;

            await _dbContext.Agents.AddAsync(agentEntity);
            await _dbContext.SaveChangesAsync();

            return agentEntity.Id;
        }

        public async Task<List<AgentDTO>> GetAllAgentsAsync()
        {
            var agents = await _dbContext.Agents.ToListAsync();

            return _mapper.Map<List<AgentDTO>>(agents);
        }
    }
}

