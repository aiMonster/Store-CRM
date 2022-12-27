using System;
using StoreCRM.Entities;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class CreateProductDTO
	{
        public string Name { get; set; }
        public string Description { get; set; }

        public int Code { get; set; }
        public string Article { get; set; }

        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public Guid CategoryId { get; set; }

        public List<Guid> Attachments { get; set; }
    }
}

