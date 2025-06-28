
namespace Chat.Bi.SharedKernel.Extensions;

public static class ConfigurationExtensions
{
    public static T GetNested<T>(this IConfiguration configuration) where T : class
    {
        return configuration.GetSection(typeof(T).Name).Get<T>();
    }
}
