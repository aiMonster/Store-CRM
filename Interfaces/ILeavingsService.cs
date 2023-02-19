using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface ILeavingsService
	{
        Task<List<LeavingDTO>> GetAllAsync();
    }
}

