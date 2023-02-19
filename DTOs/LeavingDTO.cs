using System;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class LeavingDTO
	{
        public ProductDTO Product { get; set; }

        public DateTimeOffset? LastPostingDate { get; set; }

        public PriceDTO? Cost { get; set; }

        public int InStockCount { get; set; }
    }
}

