namespace Evently.Application.Abstractions.Identity;

public record IdentityModel(string UserId, string Email, IList<string> Roles, List<string> Permissions);
