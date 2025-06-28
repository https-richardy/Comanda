namespace Comanda.Infrastructure.Gateways;

public sealed class KeycloakIdentityGateway(
    HttpClient httpClient,
    IClientAuthenticatorGateway authenticator
) : IIdentityGateway
{
    private readonly JsonSerializerOptions _options = KeycloakSerializer.SerializerOptions;

    public async Task<Result<Guid>> EnrollAsync(EnrollmentCredentials credentials)
    {
        var authentication = await authenticator.AuthenticateAsync();
        if (authentication.IsFailure)
        {
            return Result<Guid>.Failure(authentication.Error);
        }

        var token = authentication.Data!.AccessToken;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var user = new
        {
            username = credentials.Email,
            email = credentials.Email,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    temporary = false,
                    value = credentials.Password
                }
            }
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("users", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return Result<Guid>.Failure(IdentityErrors.UserAlreadyExists);
            }

            return Result<Guid>.Failure(IdentityErrors.EnrollmentFailed);
        }

        var location = response.Headers.Location;
        if (location is null || !Guid.TryParse(location.Segments.Last(), out var userId))
        {
            return Result<Guid>.Failure(IdentityErrors.EnrollmentFailed);
        }

        return Result<Guid>.Success(userId);
    }

    public async Task<Result<IEnumerable<User>>> GetUsersAsync(IdentityFilters filters)
    {
        var authentication = await authenticator.AuthenticateAsync();
        if (authentication.IsFailure)
        {
            return Result<IEnumerable<User>>.Failure(authentication.Error);
        }

        var token = authentication.Data!.AccessToken;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var queryParams = BuildQueryParams(filters);
        var url = $"users{queryParams}";

        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => Result<IEnumerable<User>>.Failure(IdentityErrors.UnauthorizedAccess),
                HttpStatusCode.Forbidden => Result<IEnumerable<User>>.Failure(IdentityErrors.ForbiddenAccess),
                HttpStatusCode.TooManyRequests => Result<IEnumerable<User>>.Failure(IdentityErrors.RateLimitExceeded),

                _ => Result<IEnumerable<User>>.Failure(IdentityErrors.FetchUsersFailed)
            };
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<User>>(responseContent, _options)!;

        return Result<IEnumerable<User>>.Success(users);
    }

    public async Task<Result> AssignRoleAsync(Guid userId, string roleName)
    {
        var authentication = await authenticator.AuthenticateAsync();
        if (authentication.IsFailure)
        {
            return Result.Failure(authentication.Error);
        }

        var token = authentication.Data!.AccessToken;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var roleResponse = await httpClient.GetAsync($"roles/{roleName}");
        if (!roleResponse.IsSuccessStatusCode)
        {
            return Result.Failure(IdentityErrors.RoleDoesNotExist);
        }

        var roleContent = await roleResponse.Content.ReadAsStringAsync();
        var role = JsonSerializer.Deserialize<RoleRepresentation>(roleContent, _options)!;

        var assignUrl = $"users/{userId}/role-mappings/realm";

        var payload = JsonSerializer.Serialize(new[] { role });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var assignResponse = await httpClient.PostAsync(assignUrl, content);
        if (!assignResponse.IsSuccessStatusCode)
        {
            return Result.Failure(IdentityErrors.RoleAssignmentFailed);
        }

        return Result.Success();
    }

    public async Task<Result> AssignUserToGroupAsync(Guid userId, string groupName)
    {
        var authentication = await authenticator.AuthenticateAsync();
        if (authentication.IsFailure)
        {
            return Result.Failure(authentication.Error);
        }

        var token = authentication.Data!.AccessToken;
        var authenticationHeader = new AuthenticationHeaderValue("Bearer", token);

        httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;

        var groupsResponse = await httpClient.GetAsync("groups");
        if (!groupsResponse.IsSuccessStatusCode)
        {
            return Result.Failure(IdentityErrors.GroupAssignmentFailed);
        }

        var json = await groupsResponse.Content.ReadAsStringAsync();
        var groups = JsonSerializer.Deserialize<List<GroupRepresentation>>(json, _options);

        var group = groups?.FirstOrDefault(group => group.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));
        if (group is null)
            return Result.Failure(IdentityErrors.GroupNotFound);

        var assignUrl = $"users/{userId}/groups/{group.Id}";
        var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync(assignUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(IdentityErrors.GroupAssignmentFailed);
        }

        return Result.Success();
    }

    private string BuildQueryParams(IdentityFilters filters)
    {
        var parameters = new List<string>();

        if (filters.UserId != Guid.Empty && filters.UserId != default)
        {
            parameters.Add($"id={filters.UserId}");
        }

        if (!string.IsNullOrWhiteSpace(filters.Email))
        {
            parameters.Add($"username={filters.Email}");
        }

        if (parameters.Count == 0)
        {
            return "";
        }

        parameters.Add($"first={filters.Skip}");
        parameters.Add($"max={filters.PageSize}");

        return "?" + string.Join("&", parameters);
    }
}