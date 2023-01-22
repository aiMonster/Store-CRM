using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.DTOs;
using StoreCRM.Entities;
using StoreCRM.Enums;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
    public class TasksService : ITasksService
    {
        private readonly IMapper _mapper;
        private readonly StoreCrmDbContext _dbContext;

        public TasksService(IMapper mapper, StoreCrmDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            var tasks = await _dbContext.Tasks
                .Include(task => task.AssignedTo)
                .ToListAsync();

            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async Task<int> CreateTaskAsync(CreateTaskDTO task)
        {
            if (task.AssignedToId != null)
            {
                var assignee = await _dbContext.Users.FindAsync(task.AssignedToId);

                if (assignee == null)
                {
                    throw new Exception("Could not find the assignee");
                }
            }

            var taskEntity = _mapper.Map<StoreTask>(task);

            await _dbContext.Tasks.AddAsync(taskEntity);
            await _dbContext.SaveChangesAsync();

            return taskEntity.Id;
        }

        public async Task RemoveTaskByIdAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                throw new Exception("Task doesn't exist");
            }

            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int taskId, StoreTaskStatus status)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                throw new Exception("Could not find the task");
            }

            task.Status = status;

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAssigneeAsync(int taskId, Guid? assigneeId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                throw new Exception("Could not find the task");
            }

            if (assigneeId != null)
            {
                var assignee = await _dbContext.Users.FindAsync(assigneeId);

                if (assignee == null)
                {
                    throw new Exception("Could not find the assignee");
                }
            }

            task.AssignedToId = assigneeId;

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}

