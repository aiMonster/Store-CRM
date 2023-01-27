using System;
using System.ComponentModel.DataAnnotations;

namespace StoreCRM.Entities
{
	public class Posting
	{
        [Key]
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public Guid AddedById { get; set; }
        public User AddedBy { get; set; }

        public List<PostingProduct> Products { get; set; }
    }
}

