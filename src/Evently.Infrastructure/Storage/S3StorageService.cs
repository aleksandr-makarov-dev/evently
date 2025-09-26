using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Evently.Application.Abstractions.Storage;
using Evently.Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evently.Infrastructure.Storage;

internal sealed class S3StorageService(
    IAmazonS3 s3Client,
    IOptions<S3Options> options,
    ILogger<S3StorageService> logger
) : IStorageService
{
    private readonly S3Options _s3Options = options.Value;

    public async Task<Result<string>> UploadAsync(
        string uniqueId,
        string fileName,
        string contentType,
        long fileSize,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        string key = $"public/{uniqueId}/{fileName}";

        var request = new PutObjectRequest
        {
            BucketName = _s3Options.BucketName,
            Key = key,
            InputStream = content,
            ContentType = contentType
        };

        logger.LogDebug(
            "Starting file upload. Bucket: {Bucket}, Key: {Key}",
            _s3Options.BucketName, key
        );

        try
        {
            PutObjectResponse? response = await s3Client.PutObjectAsync(request, cancellationToken);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                logger.LogInformation(
                    "File uploaded successfully. Bucket: {Bucket}, Key: {Key}",
                    _s3Options.BucketName, key
                );
                return Result.Success(key);
            }

            logger.LogWarning(
                "File upload failed. Bucket: {Bucket}, Key: {Key}, StatusCode: {ResponseHttpStatusCode}",
                _s3Options.BucketName, key, response.HttpStatusCode
            );

            return Result.Failure<string>(StorageErrors.UploadingFailed);
        }
        catch (AmazonS3Exception ex)
        {
            logger.LogError(
                ex,
                "S3 exception during file upload. Bucket: {Bucket}, Key: {Key}",
                _s3Options.BucketName, key
            );

            return Result.Failure<string>(StorageErrors.UploadingFailed);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected exception during file upload. Bucket: {Bucket}, Key: {Key}",
                _s3Options.BucketName, key
            );

            return Result.Failure<string>(StorageErrors.UploadingFailed);
        }
    }
}
