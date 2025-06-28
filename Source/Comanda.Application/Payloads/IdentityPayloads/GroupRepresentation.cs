namespace Comanda.Application.Payloads;

public sealed class GroupRepresentation
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Path { get; set; }
    public List<GroupRepresentation>? SubGroups { get; set; }
}
