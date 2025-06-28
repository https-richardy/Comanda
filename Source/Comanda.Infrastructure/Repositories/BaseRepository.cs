namespace Comanda.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(IMongoDatabase database, string collection) :
    IRepository<TEntity> where TEntity : Entity
{
    protected readonly IMongoCollection<TEntity> _collection = database.GetCollection<TEntity>(collection);

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.MarkAsDeleted();
        entity.MarkAsUpdatedNow();

        var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await _collection.ReplaceOneAsync(filter, entity);
        return entity;
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.MarkAsUpdatedNow();

        var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await _collection.ReplaceOneAsync(filter, entity);
        return entity;
    }
}