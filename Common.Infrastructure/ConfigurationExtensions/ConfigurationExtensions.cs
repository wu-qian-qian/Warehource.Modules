using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.ConfigurationExtensions;

/// <summary>
///     对一些json 文件的注入
/// </summary>
public static class ConfigurationExtensions
{
    public static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, string[] modules)
    {
        foreach (var module in modules)
        {
            configurationBuilder.AddJsonFile($"{module}.json", false, true);
            configurationBuilder.AddJsonFile($"{module}.Development.json", true, true);
        }
    }
}