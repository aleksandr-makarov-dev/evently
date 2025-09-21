using Evently.API.Extensions;
using Evently.API.Infrastructure;
using Evently.Application.Users.ConfirmEmail;
using Evently.Application.Users.GetCurrentUser;
using Evently.Application.Users.LoginUser;
using Evently.Application.Users.LogOut;
using Evently.Application.Users.RefreshToken;
using Evently.Application.Users.RegisterUser;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.Users;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController(
    ISender sender,
    ILogger<UsersController> logger
) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        Result<RegisterUserResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginLogin([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        Result<TokenResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        HttpContext.Response.AddRefreshTokenCookie(
            result.Value.RefreshToken,
            result.Value.RefreshTokenExpiresAtUtc);

        return Ok(new AccessTokenResponse(result.Value.AccessToken));
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        bool cookieExists = HttpContext.Request.TryGetRefreshTokenCookie(out string refreshToken);

        if (!cookieExists || string.IsNullOrEmpty(refreshToken))
        {
            logger.LogWarning("Refresh token could not be retrieved. Cookie does not exist or value is empty.");

            return Unauthorized();
        }

        var command = new RefreshTokenCommand(refreshToken);

        Result<TokenResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        HttpContext.Response.AddRefreshTokenCookie(
            result.Value.RefreshToken,
            result.Value.RefreshTokenExpiresAtUtc);

        return Ok(new AccessTokenResponse(result.Value.AccessToken));
    }


    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutUser()
    {
        bool cookieExists = HttpContext.Request.TryGetRefreshTokenCookie(out string refreshToken);

        if (!cookieExists || string.IsNullOrEmpty(refreshToken))
        {
            logger.LogWarning("Refresh token could not be retrieved. Cookie does not exist or value is empty.");

            return Unauthorized();
        }

        var command = new LogOutCommand(refreshToken);

        await sender.Send(command);

        HttpContext.Response.RemoveRefreshTokenCookie();

        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string code)
    {
        var command = new ConfirmEmailCommand(userId, code);

        await sender.Send(command);

        return Ok();
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Me()
    {
        var command = new GetCurrentUserQuery();

        Result<CurrentUserResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }
}
