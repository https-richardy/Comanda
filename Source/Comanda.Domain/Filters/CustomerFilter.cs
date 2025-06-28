namespace Comanda.Domain.Filters;

public sealed class CustomerFilters : PaginationFilter
{
    public IEnumerable<string>? Emails { get; set; }
    public IEnumerable<string>? Names { get; set; }
    public IEnumerable<Guid>? CustomerIds { get; set; }

    private CustomerFilters() { }

    public sealed class Builder
    {
        private readonly CustomerFilters _filters = new();

        public Builder WithCustomerIds(IEnumerable<Guid> customerIds)
        {
            _filters.CustomerIds = customerIds;
            return this;
        }

        public Builder WithCustomerId(Guid customerId) =>
            WithCustomerIds([customerId]);

        public Builder WithEmails(IEnumerable<string> emails)
        {
            _filters.Emails = emails;
            return this;
        }

        public Builder WithEmail(string email) =>
            WithEmails([email]);

        public Builder WithNames(IEnumerable<string> names)
        {
            _filters.Names = names;
            return this;
        }

        public Builder WithName(string name) =>
            WithNames([name]);
    }
}