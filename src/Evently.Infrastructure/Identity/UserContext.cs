using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Identity;
using Evently.Infrastructure.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Evently.Infrastructure.Identity;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    IApplicationDbContext context,
    IMemoryCache memoryCache)
    : IUserContext
{
    private const string UserKeyPrefix = "users:id:";
    private static readonly TimeSpan Duration = TimeSpan.FromMinutes(30);

    public async Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default)
    {
        string? identityId = httpContextAccessor.HttpContext?.User.GetIdentityId();

        if (identityId is null)
        {
            return Guid.Empty;
        }

        string key = $"{UserKeyPrefix}:{identityId}";

        Guid? userId = await memoryCache.GetOrCreateAsync(key, async (entry) =>
        {
            entry.SetSlidingExpiration(Duration);

            return await context.Users
                .Where(x => x.IdentityId == identityId)
                .Select(x => (Guid?)x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        });

        return userId;
    }
}
