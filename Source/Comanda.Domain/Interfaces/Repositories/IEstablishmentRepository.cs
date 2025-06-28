namespace Comanda.Domain.Interfaces.Repositories;

public interface IEstablishmentRepository : IRepository<Establishment>
{
    Task<IEnumerable<Establishment>> GetEstablishmentsAsync(EstablishmentFilters filters);
}