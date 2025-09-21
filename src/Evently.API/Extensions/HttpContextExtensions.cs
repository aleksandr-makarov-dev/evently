namespace Evently.API.Extensions;

public static class HttpContextExtensions
{
    private const string CookieName = "evently_refresh_token";

    public static void AddRefreshTokenCookie(this HttpResponse response, string token, DateTime expiresAtUtc)
    {
        response.Cookies.Append(CookieName, token, new CookieOptions
        {
            Expires = expiresAtUtc,
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        });
    }

    public static bool TryGetRefreshTokenCookie(this HttpRequest request, out string? token)
    {
        return request.Cookies.TryGetValue(CookieName, out token);
    }

    public static void RemoveRefreshTokenCookie(this HttpResponse response)
    {
        response.Cookies.Delete(CookieName, new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        });
    }
}
