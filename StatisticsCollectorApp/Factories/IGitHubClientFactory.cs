using Octokit;

namespace StatisticsCollectorApp.Factories;

public interface IGitHubClientFactory
{
    IGitHubClient GetOrCreate();
}