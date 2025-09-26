using Evently.API.Extensions;
using Evently.API.Infrastructure;
using Evently.Application.Attachments.UploadAttachment;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.Attachments;

[ApiController]
[Route("api/attachments")]
public class AttachmentsController(ISender sender) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadAttachment([FromForm] IFormFile file)
    {
        await using Stream stream = file.OpenReadStream();

        var command = new UploadAttachmentCommand(
            file.FileName,
            file.ContentType,
            file.Length,
            stream
        );

        Result<Guid> result = await sender.Send(command);

        return result.Match(
            value => Ok(new { id = value }),
            ApiResults.Problem
        );
    }
}
