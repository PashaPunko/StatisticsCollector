using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Options;

namespace StatisticsCollectorApp.StartUp;

public static class ContainerBuilderExtensions
{
    public static void RegisterOptions(this ContainerBuilder builder, IConfiguration configuration)
    {
        var services = new ServiceCollection();
        services.Configure<GitHubOptions>(configuration.GetSection(nameof(GitHubOptions)));
        services.Configure<RepositoryParameters>(configuration.GetSection(nameof(RepositoryParameters)));
        var serviceProvider = services.BuildServiceProvider();
        builder.Register(_ => serviceProvider.GetService<IOptions<GitHubOptions>>());
        builder.Register(_ => serviceProvider.GetService<IOptions<RepositoryParameters>>());
    }
}