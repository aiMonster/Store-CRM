using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StoreCRM.Context;
using StoreCRM.Entities;
using StoreCRM.Enums;
using StoreCRM.Interfaces;

namespace StoreCRM.Services
{
	public class AttachmentsService : IAttachmentsService
	{
        private readonly string _rootDir;

        private readonly StoreCrmDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentsService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, StoreCrmDbContext dbContext)
		{
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;

            _rootDir = configuration[$"AttachmentsLocation"];
        }

		public async Task<Guid> UploadAsync(IFormFile attachment)
        {
			var id = Guid.NewGuid();

            (string name, string ext) = SplitFileName(attachment.FileName);

            var attachmentEntity = new Attachment
            {
                Id = id,
                Name = name,
                Extension = ext
            };

            await SaveAttachment(attachment, $"{id}{ext}");

            await _dbContext.Attachments.AddAsync(attachmentEntity);
            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<(byte[] content, string extension)> GetByIdAsync(Guid id)
        {
            var attachment = await _dbContext.Attachments.FindAsync(id);

            if (attachment == null)
            {
                throw new Exception("Attachment doesn't exist");
            }

            var path = Path.Combine(_rootDir, $"{id}{attachment.Extension}");

            if (!File.Exists(path))
            {
                throw new Exception("Attachment has been lost");
            }

            var attachmentContent = await File.ReadAllBytesAsync(path);

            return (attachmentContent, attachment.Extension);
        }

        private (string name, string ext) SplitFileName(string fileName)
        {
            var extStartIndex = fileName.LastIndexOf('.');
            var ext = fileName.Substring(extStartIndex, fileName.Length - extStartIndex);
            var name = fileName.Substring(0, extStartIndex);

            return (name, ext);
        }

        private async Task SaveAttachment(IFormFile attachment, string name)
        {
            EnsureDirectoryExists(_rootDir);

            using (var sr = new FileStream(Path.Combine(_rootDir, name), FileMode.Create))
            {
                await attachment.CopyToAsync(sr);
            }
        }

        private void EnsureDirectoryExists(string uploadsFolder)
        {
            if (Directory.Exists(uploadsFolder))
            {
                return;
            }
            Directory.CreateDirectory(uploadsFolder);
        }
    }
}

