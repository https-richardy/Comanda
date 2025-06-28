namespace Comanda.Infrastructure.Gateways;

public sealed class KeycloakClientAuthenticatorGateway(HttpClient httpClient, ISettings settings) :
    IClientAuthenticatorGateway
{
    private readonly JsonSerializerOptions _options = KeycloakSerializer.SerializerOptions;

    public async Task<Result<AuthenticationResult>> AuthenticateAsync()
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", settings.Keycloak.ClientId },
            { "client_secret", settings.Keycloak.ClientSecret }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await httpClient.PostAsync("protocol/openid-connect/token", content);

        if (!response.IsSuccessStatusCode)
        {
            return Result<AuthenticationResult>.Failure(AuthenticationErrors.InvalidCredentials);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var authenticationResult = JsonSerializer.Deserialize<AuthenticationResult>(responseContent, _options)!;

        return Result<AuthenticationResult>.Success(authenticationResult);
    }
}