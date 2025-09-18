using Evently.API.Infrastructure;
using Evently.Application.Users.ConfirmEmail;
using Evently.Application.Users.LoginUser;
using Evently.Application.Users.LogOut;
using Evently.Application.Users.RefreshToken;
using Evently.Application.Users.RegisterUser;
using Evently.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController(ISender sender) : ControllerBase
{
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginLogin([FromBody] LoginUserRequest request)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        Result<TokenResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        // TODO: refresh token should come from httpOnly cookie
        var command = new RefreshTokenCommand(request.RefreshToken);

        Result<TokenResponse> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(result.Value);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> LogoutUser([FromBody] LogOutRequest request)
    {
        // TODO: refresh token should come from httpOnly cookie
        var command = new LogOutCommand(request.RefreshToken);

        await sender.Send(command);

        return NoContent();
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string code)
    {
        var command = new ConfirmEmailCommand(userId, code);

        await sender.Send(command);

        return Ok();
    }
}
