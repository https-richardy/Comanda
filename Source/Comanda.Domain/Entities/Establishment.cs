namespace Comanda.Domain.Entities;

public sealed class Establishment : Entity
{
    public string Name { get; set; } = default!;
    public EstablishmentOwner Owner { get; set; } = default!;
    public EstablishmentCredentials Credentials { get; set; } = default!;
    public Subscription Subscription { get; set; } = default!;

    public IList<Product> Products { get; set; } = [];
    public IList<Category> Categories { get; set; } = [];
}