using System;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class TaskDTO
	{
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public StoreTaskStatus Status { get; set; }
        public DateTimeOffset TargetDate { get; set; }

        public UserDTO AssignedTo { get; set; }
    }
}