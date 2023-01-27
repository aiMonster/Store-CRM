using System;
using StoreCRM.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoreCRM.Entities
{
	public class PostingProduct
	{
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }

        public decimal PricePerItem { get; set; }
        public Currency Currency { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PostingId { get; set; }
        public Posting Posting { get; set; }
    }
}

