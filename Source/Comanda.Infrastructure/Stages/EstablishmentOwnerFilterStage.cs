namespace Comanda.Infrastructure.Stages;

public static class EstablishmentOwnerFilterStage
{
    public static PipelineDefinition<EstablishmentOwner, BsonDocument> FilterOwners(
        this PipelineDefinition<EstablishmentOwner, BsonDocument> pipelineDefinition,
        EstablishmentOwnerFilters queryFilter
    )
    {
        var matchFilter = BuildMatchFilter(queryFilter);
        if (matchFilter != FilterDefinition<BsonDocument>.Empty)
        {
            pipelineDefinition = pipelineDefinition.Match(matchFilter);
        }

        return pipelineDefinition;
    }

    private static FilterDefinition<BsonDocument> BuildMatchFilter(EstablishmentOwnerFilters queryFilter)
    {
        var filters = new List<FilterDefinition<BsonDocument>>
        {
            MatchByOwnerIds(queryFilter),
            MatchByEmails(queryFilter)
        };

        filters.RemoveAll(filter => filter == FilterDefinition<BsonDocument>.Empty);

        return filters.Count switch
        {
            0 => FilterDefinition<BsonDocument>.Empty,
            1 => filters[0],

            _ => Builders<BsonDocument>.Filter.And(filters)
        };
    }

    private static FilterDefinition<BsonDocument> MatchByOwnerIds(EstablishmentOwnerFilters queryFilter)
    {
        if (queryFilter?.OwnerIds == null || !queryFilter.OwnerIds.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }

        var bsonGuids = queryFilter!.OwnerIds!
            .Select(id => new BsonBinaryData(id, GuidRepresentation.Standard));

        var filter = new BsonDocument("_id", new BsonDocument("$in", new BsonArray(bsonGuids)));

        return filter;
    }

    private static FilterDefinition<BsonDocument> MatchByEmails(EstablishmentOwnerFilters queryFilter)
    {
        if (queryFilter?.Emails == null || !queryFilter.Emails.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }

        var filter = new BsonDocument("Email", new BsonDocument("$in", new BsonArray(queryFilter.Emails)));

        return filter;
    }
}