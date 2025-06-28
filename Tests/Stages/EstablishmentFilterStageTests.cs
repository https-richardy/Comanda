namespace Comanda.TestSuite.Stages;

public sealed class EstablishmentFilterStageTests : IClassFixture<MongoDbFixture>, IAsyncLifetime
{
    private readonly IFixture _fixture;
    private readonly IMongoCollection<Establishment> _collection;

    public EstablishmentFilterStageTests(MongoDbFixture fixture)
    {
        _collection = fixture.Database.GetCollection<Establishment>(Collections.Establishments);

        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact(DisplayName = "Builds pipeline filtering establishments correctly by owner IDs")]
    public async Task BuildPipeline_FiltersEstablishmentsByOwnerIds_Correctly()
    {
        /* arrange: create establishments and owner */

        var ownerId = Guid.NewGuid();
        var owner = _fixture.Build<EstablishmentOwner>()
            .With(owner => owner.Id, ownerId)
            .Create();

        var establishment1 = _fixture.Create<Establishment>();
        var establishment2 = _fixture.Create<Establishment>();

        var establishment3 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Owner, owner)
            .Create();

        var establishment4 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Owner, owner)
            .Create();

        /* act: insert establishments and run pipeline */

        await _collection.InsertManyAsync(new[] { establishment1, establishment2, establishment3, establishment4 });

        var filters = new EstablishmentFilters.Builder()
            .WithOwnerIds([ownerId])
            .Build();

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

        /* assert: establishments were filtered correctly */

        Assert.NotEmpty(establishments);
        Assert.Equal(2, establishments.Count);
    }

    [Fact(DisplayName = "Builds pipeline filtering establishments correctly by establishment IDs")]
    public async Task BuildPipeline_FiltersEstablishmentsByEstablishmentIds_Correctly()
    {
        /* arrange: create establishments */

        var establishment1 = _fixture.Create<Establishment>();
        var establishment2 = _fixture.Create<Establishment>();
        var establishment3 = _fixture.Create<Establishment>();
        var establishment4 = _fixture.Create<Establishment>();

        var establishmentIdsToFilter = new[] { establishment2.Id, establishment4.Id };

        /* act: insert establishments and run pipeline */

        await _collection.InsertManyAsync(new[] { establishment1, establishment2, establishment3, establishment4 });

        var filters = new EstablishmentFilters.Builder()
            .WithEstablishmentIds(establishmentIdsToFilter)
            .Build();

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

        /* assert: establishments were filtered correctly */

        Assert.NotEmpty(establishments);
        Assert.Contains(establishments, establishment => establishment.Id == establishment2.Id);
        Assert.Contains(establishments, establishment => establishment.Id == establishment4.Id);
        Assert.Equal(2, establishments.Count);
    }

    [Fact(DisplayName = "Builds pipeline filtering establishments correctly by names")]
    public async Task BuildPipeline_FiltersEstablishmentsByEstablishmentNames_Correctly()
    {
        /* arrange: create establishments with specific names */

        var establishment1 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Name, "Pizza da Vila")
            .Create();

        var establishment2 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Name, "Café Central")
            .Create();

        var establishment3 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Name, "Restaurante do Zé")
            .Create();

        var establishment4 = _fixture.Build<Establishment>()
            .With(establishment => establishment.Name, "Padaria Imperial")
            .Create();

        var namesToFilter = new[] { "Café Central", "Padaria Imperial" };

        /* act: insert and run pipeline */

        await _collection.InsertManyAsync([establishment1, establishment2, establishment3, establishment4 ]);

        var filters = new EstablishmentFilters.Builder()
            .WithEstablishmentNames(namesToFilter)
            .Build();

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

        /* assert: names filtered correctly */

        Assert.NotEmpty(establishments);
        Assert.Equal(2, establishments.Count);

        Assert.Contains(establishments, establishment => establishment.Name == "Café Central");
        Assert.Contains(establishments, establishment => establishment.Name == "Padaria Imperial");
    }

    public async Task DisposeAsync() => await Task.CompletedTask;
    public async Task InitializeAsync()
    {
        await _collection.DeleteManyAsync(Builders<Establishment>.Filter.Empty);
    }
}