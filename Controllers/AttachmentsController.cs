using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using StoreCRM.Enums;
using StoreCRM.Interfaces;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("attachments/")]
    [ApiController]
    public class AttachmentsController : ControllerBase
	{
        private readonly IAttachmentsService _attachmentsService;

		public AttachmentsController(IAttachmentsService attachmentsService)
		{
            _attachmentsService = attachmentsService;
		}

        /// <summary>
        /// Upload an attachment
        /// </summary>
        /// <param name="attachment">An attachment to upload</param>
        /// <returns></returns>
        /// <response code="200">Uploaded attachment id</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPost]
        public async Task<ActionResult<Guid>> UploadAttachment(IFormFile attachment)
        {
            try
            {
                return Ok(await _attachmentsService.UploadAsync(attachment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get an attachment by id
        /// </summary>
        /// <param name="id">An attachment id</param>
        /// <returns></returns>
        /// <response code="200">Attachment file with an appropriate response content type</response>
        /// <response code="400">Image doesn't exist or has been lost</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAttachmentById([FromRoute]Guid id)
        {
            try
            {
                var file = await _attachmentsService.GetByIdAsync(id);
                return File(file.content, MimeTypeMap.GetMimeType(file.extension));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

