using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Categories.ArchiveCategory;

public record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
