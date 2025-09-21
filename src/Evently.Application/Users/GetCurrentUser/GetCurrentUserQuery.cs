using Evently.Application.Abstractions.Messaging;

namespace Evently.Application.Users.GetCurrentUser;

public sealed record GetCurrentUserQuery : IQuery<CurrentUserResponse>;
