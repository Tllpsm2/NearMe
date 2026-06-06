using Microsoft.Identity.Client;
using Microsoft.JSInterop;

namespace NearMe.Services;

public static class AuthService
{
    private const string AuthorityFormat =
        "https://login.microsoftonline.com/{0}/oauth2/v2.0";

    private const string AzureMapsScope =
        "https://atlas.microsoft.com/.default";

    public static string? ClientId;
    public static string? AadTenantId;
    public static string? AadAppId;
    public static string? AadClientSecret;

    internal static void SetAuthSettings(IConfigurationSection azureMaps)
    {
        ClientId = azureMaps.GetValue<string>("ClientId");
        AadTenantId = azureMaps.GetValue<string>("AadTenantId");
        AadAppId = azureMaps.GetValue<string>("AadAppId");
        AadClientSecret = azureMaps.GetValue<string>("AadClientSecret");
    }

    [JSInvokable]
    public static async Task<string> GetAccessToken()
    {
        IConfidentialClientApplication daemonClient =
            ConfidentialClientApplicationBuilder
                .Create(AadAppId)
                .WithAuthority(string.Format(AuthorityFormat, AadTenantId))
                .WithClientSecret(AadClientSecret)
                .Build();

        AuthenticationResult authResult =
            await daemonClient
                .AcquireTokenForClient(new[] { AzureMapsScope })
                .ExecuteAsync();

        return authResult.AccessToken;
    }
}

