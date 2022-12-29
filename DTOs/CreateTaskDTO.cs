using System;
namespace StoreCRM.DTOs
{
	public class CreateTaskDTO
	{
        public string Title { get; set; }
        public string Description { get; set; }

        public Enums.StoreTaskStatus Status { get; set; }
        public DateTimeOffset TargetDate { get; set; }

        public Guid? AssignedToId { get; set; }
    }
}

