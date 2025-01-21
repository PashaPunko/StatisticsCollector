using Microsoft.Extensions.Options;
using Octokit;
using StatisticsCollectorApp.Options;

namespace StatisticsCollectorApp.Factories;

public class GitHubClientFactory(IOptions<GitHubOptions> options) : IGitHubClientFactory
{
    private readonly GitHubOptions options = options.Value;
    private GitHubClient? client;

    public IGitHubClient GetOrCreate()
    {
        if (client is not null)
        {
            return client;
        }

        var credentials = string.IsNullOrEmpty(options.Token) ? Credentials.Anonymous : new Credentials(options.Token);

        client = new GitHubClient(new ProductHeaderValue(nameof(StatisticsCollectorApp)))
        {
            Credentials = credentials
        };

        return client;
    }
}