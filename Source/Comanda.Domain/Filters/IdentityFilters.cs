namespace Comanda.Domain.Filters;

public sealed class IdentityFilters : PaginationFilter
{
    public Guid? UserId { get; set; }
    public string? Email { get; set; }

    public sealed class Builder
    {
        private readonly IdentityFilters _filters = new IdentityFilters();

        public Builder WithUserId(Guid userId)
        {
            _filters.UserId = userId;
            return this;
        }

        public Builder WithEmail(string value)
        {
            _filters.Email = value;
            return this;
        }

        public IdentityFilters Build() => _filters;
    }
}