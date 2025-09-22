using Evently.Application.Abstractions.Authentication;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;

namespace Evently.Application.Users.LogOut;

internal sealed class LogOutCommandHandler(IIdentityService identityService) : ICommandHandler<LogOutCommand>
{
    public async Task<Result> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        return await identityService.LogOutUserAsync(request.RefreshToken, cancellationToken);
    }
}
