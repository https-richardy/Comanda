namespace Comanda.Application.Services;

public sealed class EstablishmentOwnerService(
    IEstablishmentRepository establishmentRepository,
    IEstablishmentOwnerRepository ownerRepository
) : IEstablishmentOwnerService
{
    public async Task<Result<OwnerDetails>> GetOwnerDetailsAsync(Guid ownerId)
    {
        var ownerFilters = new EstablishmentOwnerFilters.Builder()
            .WithOwnerId(ownerId)
            .Build();

        var establishmentFilters = new EstablishmentFilters.Builder()
            .WithOwnerId(ownerId)
            .Build();

        var owners = await ownerRepository.GetOwnersAsync(ownerFilters);
        var owner = owners.FirstOrDefault();

        var establishments = await establishmentRepository.GetEstablishmentsAsync(establishmentFilters);
        var establishment = establishments.FirstOrDefault();

        if (owner is null)
        {
            return Result<OwnerDetails>.Failure(OwnerErrors.NotFound);
        }

        if (establishment is null)
        {
            return Result<OwnerDetails>.Failure(EstablishmentErrors.NotFound);
        }

        var hasSubscription =
            establishment.Subscription is not null &&
            establishment.Subscription.Status != SubscriptionStatus.None;

        var subscriptionStatus =
            establishment?.Subscription?.Status ??
            SubscriptionStatus.None;

        var details = new OwnerDetails
        {
            HasEstablishment = establishment is not null,
            HasSubscription = hasSubscription,
            OwnerEmail = owner.Email,
            OwnerName = owner.Name,
            SubscriptionStatus = subscriptionStatus
        };

        return Result<OwnerDetails>.Success(details);
    }
}