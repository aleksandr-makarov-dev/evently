namespace Evently.Infrastructure.Storage;

internal sealed class S3Options
{
    public string Url { get; init; } = string.Empty;

    public string AccessKey { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public string BucketName { get; init; } = string.Empty;
}
