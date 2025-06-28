namespace Comanda.Domain.Filters;

public sealed class ProductFilters : PaginationFilter
{
    public IEnumerable<string>? Titles { get; set; }
    public IEnumerable<string>? Categories { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public sealed class Builder
    {
        private readonly ProductFilters _filters = new();

        public Builder WithTitles(IEnumerable<string> titles)
        {
            _filters.Titles = titles;
            return this;
        }

        public Builder WithTitle(string title) =>
            WithTitles([title]);

        public Builder WithCategories(IEnumerable<string> categories)
        {
            _filters.Categories = categories;
            return this;
        }

        public Builder WithCategory(string category) =>
            WithCategories([category]);

        public Builder WithMinPrice(decimal minPrice)
        {
            _filters.MinPrice = minPrice;
            return this;
        }

        public Builder WithMaxPrice(decimal maxPrice)
        {
            _filters.MaxPrice = maxPrice;
            return this;
        }

        public ProductFilters Build() => _filters;
    }
}