using System;
namespace StoreCRM.DTOs
{
	public class AgentDTO
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTimeOffset AddedOn { get; set; }
    }
}

