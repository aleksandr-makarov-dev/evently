using Evently.API.Infrastructure;
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

        Result<Guid> result = await sender.Send(command);

        if (result.IsFailure)
        {
            return ApiResults.Problem(this, result);
        }

        return Ok(new { Id = result.Value });
    }
}
