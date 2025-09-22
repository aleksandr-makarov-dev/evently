namespace Evently.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    TokensModel Create(IdentityModel identity);
}
