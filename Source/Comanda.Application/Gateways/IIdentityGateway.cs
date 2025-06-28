namespace Comanda.Application.Gateways;

public interface IIdentityGateway
{
    Task<Result<IEnumerable<User>>> GetUsersAsync(IdentityFilters filters);
    Task<Result<Guid>> EnrollAsync(EnrollmentCredentials credentials);
    Task<Result> AssignRoleAsync(Guid userId, string roleName);
    Task<Result> AssignUserToGroupAsync(Guid userId, string groupName);
}