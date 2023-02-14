using System;
namespace StoreCRM.DTOs
{
	public class CreatePostingDTO
	{
		public int AgentId { get; set; }

        public List<PostingNewItemDTO> Products { get; set; }
    }
}

