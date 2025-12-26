using System.Reflection;
using UrlShortener.Api.Abstractions;

namespace UrlShortener.Api.Extensions;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddHandlersFromAssembly(
        this IServiceCollection services, 
        Assembly assembly)
    {
        var concreteHandlerTypes = assembly.GetTypes()
                   .Where(type =>
                       type is { IsClass: true, IsAbstract: false } &&
                       type.IsAssignableTo(typeof(IHandler)))
                   .ToList();

        foreach (var handlerImplementation in concreteHandlerTypes)
        {
            var handlerInterface = handlerImplementation.GetInterfaces()
                .FirstOrDefault(i =>
                    i != typeof(IHandler) &&
                    i.IsAssignableTo(typeof(IHandler)));

            if (handlerInterface is not null)
            {
                services.AddScoped(handlerInterface, handlerImplementation);
            }
        }

        return services;
    }
}