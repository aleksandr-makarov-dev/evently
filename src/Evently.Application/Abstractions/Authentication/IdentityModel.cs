namespace Evently.Application.Abstractions.Authentication;

public record IdentityModel(string UserId, string Email, IList<string> Roles, List<string> Permissions);
