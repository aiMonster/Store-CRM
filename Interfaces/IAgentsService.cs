using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface IAgentsService
	{
        Task<int> AddNewAgentAsync(CreateAgentDTO agent);
        Task<List<AgentDTO>> GetAllAgentsAsync();
    }
}

