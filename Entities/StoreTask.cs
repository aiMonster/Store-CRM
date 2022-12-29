using System;
using System.ComponentModel.DataAnnotations;
using StoreCRM.Enums;

namespace StoreCRM.Entities
{
	public class StoreTask
	{
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public StoreTaskStatus Status { get; set; }
        public DateTimeOffset TargetDate { get; set; }

        public Guid? AssignedToId { get; set; }
        public User AssignedTo { get; set; }
    }
}
