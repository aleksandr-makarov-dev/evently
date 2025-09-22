namespace Evently.Application.Abstractions.Authentication;

public interface IUserContext
{
    Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default);
}
