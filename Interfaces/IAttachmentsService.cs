using System;
namespace StoreCRM.Interfaces
{
	public interface IAttachmentsService
	{
        Task<Guid> UploadAsync(IFormFile attachment);
        Task<(byte[] content, string extension)> GetByIdAsync(Guid id);
    }
}

