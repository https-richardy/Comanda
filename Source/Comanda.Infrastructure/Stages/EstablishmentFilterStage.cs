namespace Comanda.Infrastructure.Stages;

public static class EstablishmentFilterStage
{
    public static PipelineDefinition<Establishment, BsonDocument> FilterEstablishments(
        this PipelineDefinition<Establishment, BsonDocument> pipelineDefinition,
        EstablishmentFilters queryFilter
    )
    {
        var matchFilter = BuildMatchFilter(queryFilter);
        if (matchFilter != FilterDefinition<BsonDocument>.Empty)
        {
            pipelineDefinition = pipelineDefinition.Match(matchFilter);
        }

        return pipelineDefinition;
    }

    private static FilterDefinition<BsonDocument> BuildMatchFilter(EstablishmentFilters queryFilter)
    {
        var filters = new List<FilterDefinition<BsonDocument>>
        {
            MatchByOwnerIds(queryFilter),
            MatchByEstablishmentIds(queryFilter),
            MatchByEstablishmentNames(queryFilter)
        };

        filters.RemoveAll(filter => filter == FilterDefinition<BsonDocument>.Empty);

        return filters.Count switch
        {
            0 => FilterDefinition<BsonDocument>.Empty,
            1 => filters[0],

            _ => Builders<BsonDocument>.Filter.And(filters)
        };
    }

    private static FilterDefinition<BsonDocument> MatchByOwnerIds(EstablishmentFilters queryFilter)
    {
        if (queryFilter?.OwnerIds == null || !queryFilter.OwnerIds.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }

        var bsonGuids = queryFilter!.OwnerIds!
            .Select(id => new BsonBinaryData(id, GuidRepresentation.Standard));

        var filter = new BsonDocument("Owner._id", new BsonDocument("$in", new BsonArray(bsonGuids)));

        return filter;
    }

    private static FilterDefinition<BsonDocument> MatchByEstablishmentIds(EstablishmentFilters queryFilter)
    {
        if (queryFilter?.EstablishmentIds == null || !queryFilter.EstablishmentIds.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }
        var bsonGuids = queryFilter!.EstablishmentIds!
            .Select(id => new BsonBinaryData(id, GuidRepresentation.Standard));

        var filter = new BsonDocument("_id", new BsonDocument("$in", new BsonArray(bsonGuids)));

        return filter;
    }

    private static FilterDefinition<BsonDocument> MatchByEstablishmentNames(EstablishmentFilters queryFilter)
    {
        if (queryFilter?.EstablishmentNames == null || !queryFilter.EstablishmentNames.Any())
        {
            return FilterDefinition<BsonDocument>.Empty;
        }

        var filter = new BsonDocument("Name", new BsonDocument("$in", new BsonArray(queryFilter!.EstablishmentNames)));
        return filter;
    }
}