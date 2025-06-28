namespace Comanda.Infrastructure.Stages;

public static class CustomerFilterStage
{
    public static PipelineDefinition<Customer, BsonDocument> FilterCustomers(
        this PipelineDefinition<Customer, BsonDocument> pipelineDefinition,
        CustomerFilters queryFilter
    )
    {
        var matchFilter = BuildMatchFilter(queryFilter);
        if (matchFilter != FilterDefinition<BsonDocument>.Empty)
        {
            pipelineDefinition = pipelineDefinition.Match(matchFilter);
        }

        return pipelineDefinition;
    }

    private static FilterDefinition<BsonDocument> BuildMatchFilter(CustomerFilters queryFilter)
    {
        var filters = new List<FilterDefinition<BsonDocument>>
        {
            MatchByCustomerIds(queryFilter),
            MatchByEmails(queryFilter),
            MatchByNames(queryFilter),
        };

        filters.RemoveAll(filter => filter == FilterDefinition<BsonDocument>.Empty);

        return filters.Count switch
        {
            0 => FilterDefinition<BsonDocument>.Empty,
            1 => filters[0],

            _ => Builders<BsonDocument>.Filter.And(filters)
        };
    }

    private static FilterDefinition<BsonDocument> MatchByCustomerIds(CustomerFilters queryFilter)
    {
        if (queryFilter?.CustomerIds == null || !queryFilter.CustomerIds.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }

        var bsonGuids = queryFilter!.CustomerIds!
            .Select(id => new BsonBinaryData(id, GuidRepresentation.Standard));

        var filter = new BsonDocument("_id", new BsonDocument("$in", new BsonArray(bsonGuids)));

        return filter;
    }

    private static FilterDefinition<BsonDocument> MatchByEmails(CustomerFilters queryFilter)
    {
        if (queryFilter?.Emails == null || !queryFilter.Emails.Any())
            return FilterDefinition<BsonDocument>.Empty;

        return new BsonDocument("Email", new BsonDocument("$in", new BsonArray(queryFilter.Emails)));
    }

    private static FilterDefinition<BsonDocument> MatchByNames(CustomerFilters queryFilter)
    {
        if (queryFilter?.Names == null || !queryFilter.Names.Any())
            return FilterDefinition<BsonDocument>.Empty;

        return new BsonDocument("Name", new BsonDocument("$in", new BsonArray(queryFilter.Names)));
    }
}