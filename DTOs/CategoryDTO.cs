using System;
namespace StoreCRM.DTOs
{
	public class CategoryDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public Guid? ParentId { get; set; }
		public List<CategoryDTO> Children { get; set; }
	}
}

