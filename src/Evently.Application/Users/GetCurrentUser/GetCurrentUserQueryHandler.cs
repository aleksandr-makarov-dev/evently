using Evently.Application.Abstractions.Data;
using Evently.Application.Abstractions.Identity;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Evently.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Evently.Application.Users.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    public async Task<Result<CurrentUserResponse>> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        Guid? userId = await userContext.GetUserIdAsync(cancellationToken);

        if (userId is null)
        {
            return Result.Failure<CurrentUserResponse>(UserErrors.Unauthorized);
        }

        CurrentUserResponse? user = await context.Users
            .Where(x => x.Id == userId.Value)
            .Select(x => new CurrentUserResponse(
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email
            ))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user is null)
        {
            return Result.Failure<CurrentUserResponse>(UserErrors.UserNotFound(userId.Value));
        }

        return Result.Success(user);
    }
}
