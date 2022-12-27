using System;
using StoreCRM.Entities;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class ProductDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int Code { get; set; }
        public string Article { get; set; }

        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public UserDTO AddedBy { get; set; }
        public CategoryInfoDTO Category { get; set; }
        public List<AttachmentDTO> Attachments { get; set; }
    }
}

