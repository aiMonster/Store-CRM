using System;
using System.ComponentModel.DataAnnotations;

namespace StoreCRM.Entities
{
	public class Stock
	{
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsRemoved { get; set; }
    }
}

