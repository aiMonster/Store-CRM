using System;
using System.ComponentModel.DataAnnotations;

namespace StoreCRM.Entities
{
	public class Category
	{
        [Key]
        public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public string Name { get; set; }
	}
}

