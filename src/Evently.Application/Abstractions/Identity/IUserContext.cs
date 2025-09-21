namespace Evently.Application.Abstractions.Identity;

public interface IUserContext
{
    Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default);
}
