namespace Comanda.Infrastructure.IoC.Helpers;

public static class MongoMapping
{
    private static bool _registered;

    public static void Register()
    {
        if (_registered)
        {
            return;
        }

        _registered = true;

        if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
        {
            BsonClassMap.RegisterClassMap<Entity>(mapper =>
            {
                var serializer = new GuidSerializer(GuidRepresentation.Standard);

                mapper.AutoMap();
                mapper.MapMember(entity => entity.Id)
                    .SetSerializer(serializer);
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(EstablishmentOwner)))
        {
            BsonClassMap.RegisterClassMap<EstablishmentOwner>(mapper =>
            {
                mapper.AutoMap();
            });
        }
    }
}
