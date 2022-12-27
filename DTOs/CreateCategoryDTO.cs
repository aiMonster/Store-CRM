using System;
namespace StoreCRM.DTOs
{
	public class CreateCategoryDTO
	{
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }
}

