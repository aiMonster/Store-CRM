using System;
namespace StoreCRM.DTOs
{
	public class CategoryDTO : CategoryInfoDTO
	{
		public int? ParentId { get; set; }
		public List<CategoryDTO> Children { get; set; }
	}
}

