namespace Evently.Domain.Users;

public class Role
{
    public static readonly Role Administrator = new(nameof(Administrator));

    public static readonly Role Member = new(nameof(Member));

    private Role(string name)
    {
        Name = name;
    }

    private Role()
    {
    }

    public string Name { get; private set; }
}
