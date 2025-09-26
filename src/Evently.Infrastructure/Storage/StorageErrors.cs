using Evently.Domain.Abstractions;

namespace Evently.Infrastructure.Storage;

public static class StorageErrors
{
    public static Error UploadingFailed = Error.Problem("FileStorage.UploadingFailed", "Failed to upload file");
}
