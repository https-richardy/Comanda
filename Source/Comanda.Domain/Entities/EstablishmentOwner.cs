namespace Comanda.Domain.Entities;

public sealed class EstablishmentOwner : Entity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}