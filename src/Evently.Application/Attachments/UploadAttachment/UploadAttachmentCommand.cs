using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Attachments.UploadAttachment;

public sealed record UploadAttachmentCommand(
    string FileName,
    string ContentType,
    long FileSize,
    Stream Content
) : ICommand<Guid>;
