using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Categories.CreateCategory;

public record CreateCategoryCommand(string Name) : ICommand<Guid>;
