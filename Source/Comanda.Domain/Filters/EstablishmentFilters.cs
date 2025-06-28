namespace Comanda.Domain.Filters;

public sealed class EstablishmentFilters : PaginationFilter
{
    public IEnumerable<Guid>? OwnerIds { get; private set; }
    public IEnumerable<Guid>? EstablishmentIds { get; private set; }
    public IEnumerable<string>? EstablishmentNames { get; private set; }

    private EstablishmentFilters() { }

    public sealed class Builder
    {
        private readonly EstablishmentFilters _filters = new();

        public Builder WithOwnerIds(IEnumerable<Guid> ownerIds)
        {
            _filters.OwnerIds = ownerIds;
            return this;
        }

        public Builder WithOwnerId(Guid ownerId) =>
            WithOwnerIds([ownerId]);

        public Builder WithEstablishmentIds(IEnumerable<Guid> ids)
        {
            _filters.EstablishmentIds = ids;
            return this;
        }

        public Builder WithEstablishmentId(Guid id) =>
            WithEstablishmentIds([id]);

        public Builder WithEstablishmentNames(IEnumerable<string> names)
        {
            _filters.EstablishmentNames = names;
            return this;
        }

        public Builder WithEstablishmentName(string name) =>
            WithEstablishmentNames([name]);

        public EstablishmentFilters Build() => _filters;
    }
}