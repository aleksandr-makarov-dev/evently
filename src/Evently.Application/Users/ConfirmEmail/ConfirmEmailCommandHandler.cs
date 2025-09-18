using Evently.Application.Abstractions.Identity;
using Evently.Application.Abstractions.Messaging;
using Evently.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Evently.Application.Users.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(
    IIdentityService identityService,
    ILogger<ConfirmEmailCommandHandler> logger
) : ICommandHandler<ConfirmEmailCommand>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        return await identityService.ConfirmEmailAsync(request.UserId, request.Code, cancellationToken);
    }
}
