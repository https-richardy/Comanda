namespace Comanda.Infrastructure.Gateways;

public sealed class KeycloakAuthenticationGateway(HttpClient httpClient, ISettings settings) : IAuthenticationGateway
{
    public async Task<Result<AuthenticationResult>> AuthenticateAsync(AuthenticationCredentials credentials)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", settings.Keycloak.ClientId },
            { "client_secret", settings.Keycloak.ClientSecret },
            { "username", credentials.Email },
            { "password", credentials.Password }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await httpClient.PostAsync("protocol/openid-connect/token", content);

        if (!response.IsSuccessStatusCode)
        {
            return Result<AuthenticationResult>.Failure(AuthenticationErrors.InvalidCredentials);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var authenticationResult = JsonSerializer.Deserialize<AuthenticationResult>(responseContent, KeycloakSerializer.SerializerOptions)!;

        return Result<AuthenticationResult>.Success(authenticationResult);
    }

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { "client_id", settings.Keycloak.ClientId },
            { "client_secret", settings.Keycloak.ClientSecret },
            { "refresh_token", refreshToken }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await httpClient.PostAsync("protocol/openid-connect/logout", content);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(AuthenticationErrors.LogoutFailed);
        }

        return Result.Success();
    }

    public async Task<Result<AuthenticationResult>> RefreshTokenAsync(string refreshToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "client_id", settings.Keycloak.ClientId },
            { "client_secret", settings.Keycloak.ClientSecret },
            { "refresh_token", refreshToken }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await httpClient.PostAsync("protocol/openid-connect/token", content);

        if (!response.IsSuccessStatusCode)
        {
            return Result<AuthenticationResult>.Failure(AuthenticationErrors.InvalidRefreshToken);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var authenticationResult = JsonSerializer.Deserialize<AuthenticationResult>(responseContent, KeycloakSerializer.SerializerOptions)!;

        return Result<AuthenticationResult>.Success(authenticationResult);
    }
}