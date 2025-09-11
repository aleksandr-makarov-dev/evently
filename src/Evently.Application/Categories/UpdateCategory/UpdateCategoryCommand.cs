using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Categories.UpdateCategory;

public record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
