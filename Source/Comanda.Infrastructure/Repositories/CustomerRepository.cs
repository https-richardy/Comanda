namespace Comanda.Infrastructure.Repositories;

public sealed class CustomerRepository(IMongoDatabase database) :
    BaseRepository<Customer>(database, Collections.Customers),
    ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetCustomersAsync(CustomerFilters filters)
    {
        var pipeline = PipelineDefinitionBuilder
            .For<Customer>()
            .As<Customer, Customer, BsonDocument>()
            .FilterCustomers(filters);

        var options = new AggregateOptions { AllowDiskUse = true };
        var aggregation = await _collection.AggregateAsync(pipeline, options);

        var bsonDocuments = await aggregation.ToListAsync();
        var customers = bsonDocuments
            .Select(bsonDocument => BsonSerializer.Deserialize<Customer>(bsonDocument))
            .ToList();

        return customers;
    }
}