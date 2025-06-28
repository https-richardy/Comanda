namespace Comanda.Domain.Entities;

public sealed class Product : Entity
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Image Image { get; set; } = default!;
    public Category Category { get; set; } = default!;
}