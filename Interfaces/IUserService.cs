using System;
using StoreCRM.DTOs;

namespace StoreCRM.Interfaces
{
	public interface IUserService
	{
        Task<string> GetTokenAsync(LoginDTO user);
        Task AddNewUserAsync(RegisterDTO user);
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}

