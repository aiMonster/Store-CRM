using System;
using StoreCRM.DTOs;
using StoreCRM.Enums;

namespace StoreCRM.Interfaces
{
	public interface ITasksService
	{
        Task<List<TaskDTO>> GetAllAsync();
        Task<int> CreateTaskAsync(CreateTaskDTO task);
        Task UpdateStatusAsync(int taskId, StoreTaskStatus status);
        Task UpdateAssigneeAsync(int taskId, Guid? assigneeId);
    }
}

