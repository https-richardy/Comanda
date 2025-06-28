namespace Comanda.Application.Handlers;

public sealed class GetOwnerDetailsHandler(
    IEstablishmentOwnerService ownerService,
    IEstablishmentOwnerRepository ownerRepository,
    IAuthenticatedUserProvider userProvider
) : IRequestHandler<GetOwnerDetails, Result<OwnerDetails>>
{
    public async Task<Result<OwnerDetails>> Handle(GetOwnerDetails request, CancellationToken cancellationToken)
    {
        var user = await userProvider.GetUserAsync();
        var filters = new EstablishmentOwnerFilters.Builder()
            .WithEmail(user.Email)
            .Build();

        var owners = await ownerRepository.GetOwnersAsync(filters);
        var owner = owners.FirstOrDefault();

        if (owner is null)
        {
            return Result<OwnerDetails>.Failure(OwnerErrors.NotFound);
        }

        var result = await ownerService.GetOwnerDetailsAsync(owner.Id);
        var details = result.Data!;

        if (result.IsFailure)
        {
            return Result<OwnerDetails>.Failure(result.Error);
        }

        return Result<OwnerDetails>.Success(details);
    }
}