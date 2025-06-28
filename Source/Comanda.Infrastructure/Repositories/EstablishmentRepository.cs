namespace Comanda.Infrastructure.Repositories;

public sealed class EstablishmentRepository(IMongoDatabase database) :
    BaseRepository<Establishment>(database, Collections.Establishments),
    IEstablishmentRepository
{
    public async Task<IEnumerable<Establishment>> GetEstablishmentsAsync(EstablishmentFilters filters)
    {
        var pipeline = PipelineDefinitionBuilder
            .For<Establishment>()
            .As<Establishment, Establishment, BsonDocument>()
            .FilterEstablishments(filters);

        var options = new AggregateOptions { AllowDiskUse = true };
        var aggregation = await _collection.AggregateAsync(pipeline, options);

        var bsonDocuments = await aggregation.ToListAsync();
        var establishments = bsonDocuments
            .Select(bsonDocument => BsonSerializer.Deserialize<Establishment>(bsonDocument))
            .ToList();

        return establishments;
    }
}