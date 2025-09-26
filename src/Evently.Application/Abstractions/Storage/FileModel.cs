namespace Evently.Application.Abstractions.Storage;

public record FileModel(string FileName, string ContentType, long Size, Stream Content);
