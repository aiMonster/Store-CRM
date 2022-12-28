using System;
namespace StoreCRM.DTOs
{
	public class CreateCategoryDTO
	{
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}

