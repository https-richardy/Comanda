namespace Comanda.Application.Payloads;

public sealed record RoleRepresentation
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}
