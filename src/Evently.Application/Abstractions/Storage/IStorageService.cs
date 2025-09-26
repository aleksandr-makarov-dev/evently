using Evently.Domain.Abstractions;

namespace Evently.Application.Abstractions.Storage;

public interface IStorageService
{
    Task<Result<string>> UploadAsync(
        string uniqueId,
        string fileName,
        string contentType,
        long fileSize,
        Stream content,
        CancellationToken cancellationToken = default);
}
