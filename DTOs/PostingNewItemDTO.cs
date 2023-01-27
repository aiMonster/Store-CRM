using System;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class PostingNewItemDTO
	{
        public int ProductId { get; set; }

        public int Count { get; set; }

        public decimal PricePerItem { get; set; }

        public Currency Currency { get; set; }
    }
}

