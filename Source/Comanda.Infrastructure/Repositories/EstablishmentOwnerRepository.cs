namespace Comanda.Infrastructure.Repositories;

public sealed class EstablishmentOwnerRepository(IMongoDatabase database) :
    BaseRepository<EstablishmentOwner>(database, Collections.Owners),
    IEstablishmentOwnerRepository
{
    public async Task<IEnumerable<EstablishmentOwner>> GetOwnersAsync(EstablishmentOwnerFilters filters)
    {
        var pipeline = PipelineDefinitionBuilder
            .For<EstablishmentOwner>()
            .As<EstablishmentOwner, EstablishmentOwner, BsonDocument>()
            .FilterOwners(filters);

        var options = new AggregateOptions { AllowDiskUse = true };
        var aggregation = await _collection.AggregateAsync(pipeline, options);

        var bsonDocuments = await aggregation.ToListAsync();
        var owners = bsonDocuments
            .Select(bsonDocument => BsonSerializer.Deserialize<EstablishmentOwner>(bsonDocument))
            .ToList();

        return owners;
    }
}
