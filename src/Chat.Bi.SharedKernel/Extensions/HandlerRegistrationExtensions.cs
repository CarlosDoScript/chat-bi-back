namespace Chat.Bi.SharedKernel.Extensions;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddRequestHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var handlerInterface = typeof(IRequestHandler<,>);

        var handlerTypes = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces(), (type, iface) => new { type, iface })
            .Where(x => x.iface.IsGenericType && x.iface.GetGenericTypeDefinition() == handlerInterface);

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.iface, handler.type);
        }

        return services;
    }
}