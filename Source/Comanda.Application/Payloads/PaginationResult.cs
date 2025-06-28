namespace Comanda.Application.Payloads;

public sealed record PaginationResult<TData>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public IReadOnlyCollection<TData> Data { get; init; } = [  ];
}