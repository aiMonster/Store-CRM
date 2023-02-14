using System;
namespace StoreCRM.DTOs
{
	public class CreateAgentDTO
	{
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}

