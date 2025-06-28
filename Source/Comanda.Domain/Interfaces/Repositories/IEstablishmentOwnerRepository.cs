namespace Comanda.Domain.Interfaces.Repositories;

public interface IEstablishmentOwnerRepository : IRepository<EstablishmentOwner>
{
    Task<IEnumerable<EstablishmentOwner>> GetOwnersAsync(EstablishmentOwnerFilters filters);
}