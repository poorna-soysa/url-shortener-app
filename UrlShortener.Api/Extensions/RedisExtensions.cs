namespace UrlShortener.Api.Extensions;

public static class RedisExtensions
{
    private const string ConnectionString = "shortenUrl-cache";
    private const string InstanceName = "Shorten-Url";
    public static IServiceCollection AddRedisConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(
             options =>
             {
                 options.Configuration = configuration.GetConnectionString(ConnectionString);
                 options.InstanceName = InstanceName;
             });

        return services;
    }
}
