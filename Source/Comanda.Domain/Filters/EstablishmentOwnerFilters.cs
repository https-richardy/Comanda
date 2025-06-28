namespace Comanda.Domain.Filters;

public sealed class EstablishmentOwnerFilters
{
    public IEnumerable<Guid>? OwnerIds { get; set; }
    public IEnumerable<string>? Emails { get; set; }

    private EstablishmentOwnerFilters() { }

    public static Builder Create() => new();

    public sealed class Builder
    {
        private readonly EstablishmentOwnerFilters _filters = new();

        public Builder WithOwnerIds(IEnumerable<Guid> ownerIds)
        {
            _filters.OwnerIds = ownerIds;
            return this;
        }

        public Builder WithOwnerId(Guid ownerId) =>
            WithOwnerIds([ownerId]);

        public Builder WithEmails(IEnumerable<string> emails)
        {
            _filters.Emails = emails;
            return this;
        }

        public Builder WithEmail(string email) =>
            WithEmails([email]);

        public EstablishmentOwnerFilters Build() => _filters;
    }
}