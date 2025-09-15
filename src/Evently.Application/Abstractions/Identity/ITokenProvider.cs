namespace Evently.Application.Abstractions.Identity;

public interface ITokenProvider
{
    TokenModel Create(IdentityModel identity);
}
