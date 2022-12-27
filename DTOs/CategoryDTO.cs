using System;
namespace StoreCRM.DTOs
{
	public class CategoryDTO : CategoryInfoDTO
	{
		public Guid? ParentId { get; set; }
		public List<CategoryDTO> Children { get; set; }
	}
}

