using System;
using System.ComponentModel.DataAnnotations;
using StoreCRM.Enums;

namespace StoreCRM.Entities
{
	public class Product
	{
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int Code { get; set; }
        public string Article { get; set; }

        public decimal Price { get; set; }
        public Currency Currency { get; set; }

        public Guid AddedById { get; set; }
        public User AddedBy { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<PostingProduct> Postings { get; set; }
    }
}

