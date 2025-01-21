using Autofac;
using StatisticsCollectorApp.Factories;
using StatisticsCollectorApp.Services;

namespace StatisticsCollectorApp;

public class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GitHubRepositoryAnalyzer>().As<IGitHubRepositoryAnalyzer>();
        builder.RegisterType<GitHubContentService>().As<IGitHubContentService>();
        builder.RegisterType<RepositoryContentIterator>().As<IRepositoryContentIterator>();
        builder.RegisterType<LetterStatisticsCollector>().As<IStatisticsCollector>();
        builder.RegisterType<GitHubClientFactory>().As<IGitHubClientFactory>().SingleInstance();
        builder.RegisterType<ConsoleStatisticsPublisher>().As<IStatisticsPublisher>();
    }
}