using System.Text.Json;
using Autofac;
using StatisticsCollectorApp;
using StatisticsCollectorApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Octokit;
using StatisticsCollectorApp.Services;
using StatisticsCollectorApp.StartUp;

var builder = new ContainerBuilder();
var configuration = new ConfigurationBuilder().AddDefaultConfiguration().Build();

builder.RegisterOptions(configuration);
builder.RegisterModule<AppModule>();

var container = builder.Build();
var parameters = container.Resolve<IOptions<RepositoryParameters>>().Value;
try
{
    var letterStatistics = await container.Resolve<IGitHubRepositoryAnalyzer>().CollectStatisticsAsync(parameters);
    container.Resolve<IStatisticsPublisher>().PublishStatistics(letterStatistics);
    Environment.Exit(default);
}
catch (NotFoundException ex)
{
    Console.WriteLine($"No repository was found for parameters: {JsonSerializer.Serialize(parameters)}");
    Environment.Exit(ex.HResult);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Environment.Exit(ex.HResult);
}