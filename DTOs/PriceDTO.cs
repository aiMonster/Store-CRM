using System;
using StoreCRM.Enums;

namespace StoreCRM.DTOs
{
	public class PriceDTO
	{
        public decimal Value { get; set; }

        public Currency Currency { get; set; }
    }
}

