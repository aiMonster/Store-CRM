using System;
using StoreCRM.Entities;

namespace StoreCRM.DTOs
{
	public class PostingDTO
	{
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public StockDTO Stock { get; set; }

        public UserDTO AddedBy { get; set; }

        public List<PriceDTO> Totals { get; set; }
    }
}

