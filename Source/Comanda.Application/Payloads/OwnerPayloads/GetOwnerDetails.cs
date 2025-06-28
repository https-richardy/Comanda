namespace Comanda.Application.Payloads;

public sealed record GetOwnerDetails : IRequest<Result<OwnerDetails>>;