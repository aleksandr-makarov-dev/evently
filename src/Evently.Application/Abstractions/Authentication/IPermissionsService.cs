namespace Evently.Application.Abstractions.Authentication;

public interface IPermissionManager
{
    Task<List<string?>> GetPermissionsAsync(string userId, CancellationToken cancellationToken = default);
}
