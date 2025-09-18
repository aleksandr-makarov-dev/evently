namespace Evently.API.Controllers.Users;

public sealed record ConfirmEmailRequest(string Email, string Code);
