using Microsoft.Extensions.Configuration;

namespace StatisticsCollectorApp.StartUp;

public static class ConfigurationBuilderExtensions
{
    private const string ConfigurationFileName = "appsettings.json";

    public static IConfigurationBuilder AddDefaultConfiguration(this IConfigurationBuilder builder)
    {
        builder.AddJsonFile(ConfigurationFileName, false, true)
            .AddCommandLine(Environment.GetCommandLineArgs());

        return builder;
    }
}