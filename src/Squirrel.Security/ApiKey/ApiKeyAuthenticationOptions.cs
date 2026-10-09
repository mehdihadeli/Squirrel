using Microsoft.AspNetCore.Authentication;

namespace Squirrel.Security.ApiKey;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKey";
    public string AuthenticationType = DefaultScheme;
    public string Scheme => DefaultScheme;
}
