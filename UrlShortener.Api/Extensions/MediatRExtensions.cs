namespace UrlShortener.Api.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        return services;
    }
}