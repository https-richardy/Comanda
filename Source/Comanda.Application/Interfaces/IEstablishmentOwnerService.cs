namespace Comanda.Application.Interfaces;

public interface IEstablishmentOwnerService
{
    Task<Result<OwnerDetails>> GetOwnerDetailsAsync(Guid ownerId);
}