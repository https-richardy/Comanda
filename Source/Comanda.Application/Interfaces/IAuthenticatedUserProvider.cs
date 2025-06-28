namespace Comanda.Application.Interfaces;

public interface IAuthenticatedUserProvider
{
    Task<User> GetUserAsync();
}