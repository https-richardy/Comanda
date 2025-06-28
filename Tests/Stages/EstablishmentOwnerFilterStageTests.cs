namespace Comanda.TestSuite.Stages;

public sealed class EstablishmentOwnerFilterStageTests : IClassFixture<MongoDbFixture>, IAsyncLifetime
{
    private readonly IFixture _fixture;
    private readonly IMongoCollection<EstablishmentOwner> _collection;

    public EstablishmentOwnerFilterStageTests(MongoDbFixture fixture)
    {
        _collection = fixture.Database.GetCollection<EstablishmentOwner>(Collections.Owners);

        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact(DisplayName = "Builds pipeline filtering establishment owners correctly by owner IDs")]
    public async Task BuildPipeline_FiltersOwnersByOwnerIds_Correctly()
    {
        /* arrange: create owners */

        var ownerId = Guid.NewGuid();

        var owner1 = _fixture.Create<EstablishmentOwner>();
        var owner2 = _fixture.Build<EstablishmentOwner>()
            .With(owner => owner.Id, ownerId)
            .Create();

        var owner3 = _fixture.Create<EstablishmentOwner>();
        var owner4 = _fixture.Build<EstablishmentOwner>()
            .With(owner => owner.Id, Guid.NewGuid())
            .Create();

        /* act: insert and run pipeline */

        await _collection.InsertManyAsync(new[] { owner1, owner2, owner3, owner4 });

        var filters = EstablishmentOwnerFilters.Create()
            .WithOwnerIds(new[] { ownerId })
            .Build();

        var pipeline = PipelineDefinitionBuilder
            .For<EstablishmentOwner>()
            .As<EstablishmentOwner, EstablishmentOwner, BsonDocument>()
            .FilterOwners(filters);

        var options = new AggregateOptions { AllowDiskUse = true };
        var aggregation = await _collection.AggregateAsync(pipeline, options);

        var bsonDocuments = await aggregation.ToListAsync();
        var owners = bsonDocuments
            .Select(document => BsonSerializer.Deserialize<EstablishmentOwner>(document))
            .ToList();

        /* assert only owners with the specified ID are returned */

        Assert.NotEmpty(owners);
        Assert.All(owners, owner => Assert.Equal(ownerId, owner.Id));
    }

    [Fact(DisplayName = "Builds pipeline filtering establishment owners correctly by emails")]
    public async Task BuildPipeline_FiltersOwnersByEmails_Correctly()
    {
        /* arrange: create owners with specific emails */

        var email1 = "owner1@example.com";
        var email2 = "owner2@example.com";

        var owner1 = _fixture.Build<EstablishmentOwner>()
            .With(owner => owner.Email, email1)
            .Create();

        var owner2 = _fixture.Build<EstablishmentOwner>()
            .With(owner => owner.Email, email2)
            .Create();

        var owner3 = _fixture.Create<EstablishmentOwner>();
        var owner4 = _fixture.Create<EstablishmentOwner>();

        /* act: insert and run pipeline */

        await _collection.InsertManyAsync(new[] { owner1, owner2, owner3, owner4 });

        var filters = EstablishmentOwnerFilters.Create()
            .WithEmails(new[] { email1, email2 })
            .Build();

        var pipeline = PipelineDefinitionBuilder
            .For<EstablishmentOwner>()
            .As<EstablishmentOwner, EstablishmentOwner, BsonDocument>()
            .FilterOwners(filters);

        var options = new AggregateOptions { AllowDiskUse = true };
        var aggregation = await _collection.AggregateAsync(pipeline, options);

        var bsonDocuments = await aggregation.ToListAsync();
        var owners = bsonDocuments
            .Select(doc => BsonSerializer.Deserialize<EstablishmentOwner>(doc))
            .ToList();

        /* assert: assert that the correct owners were deserialized */

        Assert.NotEmpty(owners);
        Assert.Equal(2, owners.Count);

        Assert.Contains(owners, owner => owner.Email == email1);
        Assert.Contains(owners, owner => owner.Email == email2);
    }

    public async Task DisposeAsync() => await Task.CompletedTask;
    public async Task InitializeAsync()
    {
        await _collection.DeleteManyAsync(Builders<EstablishmentOwner>.Filter.Empty);
    }
}
