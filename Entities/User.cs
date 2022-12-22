using System;
using System.ComponentModel.DataAnnotations;

namespace StoreCRM.Entities
{
	public class User
	{
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

