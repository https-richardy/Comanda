namespace Comanda.Shared.Configuration;

public interface ISettings
{
    MongoSettings Mongo { get; }
    KeycloakSettings Keycloak { get; }
}