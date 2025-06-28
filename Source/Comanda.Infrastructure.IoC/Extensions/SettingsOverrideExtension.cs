namespace Comanda.Infrastructure.IoC.Extensions;

public static class SettingsOverrideExtension
{
    public static void OverrideSettingsWithEnvironmentVariables(this ISettings settings)
    {
        settings.Mongo.ConnectionString = GetEnvironmentVariableOrFallback("Mongo__ConnectionString", settings.Mongo.ConnectionString);
        settings.Mongo.DatabaseName = GetEnvironmentVariableOrFallback("Mongo__DatabaseName", settings.Mongo.DatabaseName);
    }

    private static string GetEnvironmentVariableOrFallback(string variableName, string fallback)
    {
        var value = Environment.GetEnvironmentVariable(variableName);
        return string.IsNullOrEmpty(value) ? fallback : value;
    }
}
