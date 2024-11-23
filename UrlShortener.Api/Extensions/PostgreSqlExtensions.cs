namespace UrlShortener.Api.Extensions;
public static class PostgreSqlExtensions
{
    private const string ConnectionString = "shortenUrl-db";
    public static IServiceCollection AddPostgreSqlConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString(ConnectionString)));

        return services;
    }
}