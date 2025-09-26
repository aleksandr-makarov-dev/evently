using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Messaging;
using Evently.Application.Abstractions.Storage;
using Evently.Domain.Abstractions;
using Evently.Domain.Attachments;

namespace Evently.Application.Attachments.UploadAttachment;

internal sealed class UploadAttachmentCommandHandler(IStorageService storageService, IApplicationDbContext context)
    : ICommandHandler<UploadAttachmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachmentId = Guid.NewGuid();

        Result<string> fileKey = await storageService.UploadAsync(
            attachmentId.ToString(),
            request.FileName,
            request.ContentType,
            request.FileSize,
            request.Content,
            cancellationToken
        );

        if (fileKey.IsFailure)
        {
            return Result.Failure<Guid>(fileKey.Error);
        }

        var attachment = new Attachment
        {
            Id = attachmentId,
            Key = fileKey.Value,
            Name = request.FileName,
            ContentType = request.ContentType,
            Size = request.FileSize
        };

        context.Attachments.Add(attachment);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(attachment.Id);
    }
}
