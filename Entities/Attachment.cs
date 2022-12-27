using System;
using System.ComponentModel.DataAnnotations;
using StoreCRM.Enums;

namespace StoreCRM.Entities
{
	public class Attachment
	{
        [Key]
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Extension { get; set; }
	}
}

