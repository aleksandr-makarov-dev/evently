namespace Evently.Domain.Users;

public sealed class Role(string name)
{
    public static readonly Role Administrator = new(nameof(Administrator));

    public static readonly Role Member = new(nameof(Member));

    public string Name { get; private set; } = name;
}
