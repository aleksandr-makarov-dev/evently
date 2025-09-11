using Evently.Domain.Abstractions;

namespace Evently.Domain.Events.Categories;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    public bool IsArchived { get; private set; }

    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsArchived = false
        };

        return category;
    }

    public void ChangeName(string name)
    {
        if (Name == name)
        {
            return;
        }

        Name = name;
    }

    public void Archive()
    {
        IsArchived = true;
    }
}
