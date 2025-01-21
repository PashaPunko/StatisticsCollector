using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StatisticsCollectorApp;
using StatisticsCollectorApp.Models;
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
catch (InvalidOperationException ex)
{
    Console.WriteLine("Error: " + ex.Message);
    Console.WriteLine("Inner error: " + ex.InnerException?.Message);
    Console.WriteLine("Stack Trace: " + ex.StackTrace);
    Console.WriteLine("Inner Stack Trace: " + ex.InnerException?.StackTrace);
    Environment.Exit(ex.HResult);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine("Error: " + ex.Message);
    Console.WriteLine("Stack Trace: " + ex.StackTrace);
    Environment.Exit(ex.HResult);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Impossible repository parameters: " + ex.Message);
    Console.WriteLine("Stack Trace: " + ex.StackTrace);
    Environment.Exit(ex.HResult);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
    Console.WriteLine("Stack Trace: " + ex.StackTrace);
    Environment.Exit(ex.HResult);
}