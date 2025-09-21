namespace Evently.Application.Abstractions.Identity;

public interface ITokenProvider
{
    TokensModel Create(IdentityModel identity);
}
