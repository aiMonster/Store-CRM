using System;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class PostingItemDTO
	{
        public int Id { get; set; }

        public ProductDTO Product { get; set; }

        public int Count { get; set; }

        public decimal PricePerItem { get; set; }

        public Currency Currency { get; set; }
    }
}

