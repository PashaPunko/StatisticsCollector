using System.Text;
using Octokit;
using StatisticsCollectorApp.Factories;
using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class GitHubContentService(IGitHubClientFactory clientFactory) : IGitHubContentService
{
    private readonly IGitHubClient client = clientFactory.GetOrCreate();

    public async Task<IEnumerable<GitHubRepositoryNodeInfo>> GetAllContentByPathAsync(RepositoryParameters parameters)
    {
        var contents = !parameters.Path.Any()
            ? await client.Repository.Content.GetAllContents(parameters.Name, parameters.Owner)
            : await client.Repository.Content.GetAllContents(parameters.Name, parameters.Owner, parameters.Path);
        var nodeInfo = contents.Select(GitHubRepositoryNodeInfo.Create);

        return nodeInfo;
    }

    public async Task<string> GetRawContentAsync(RepositoryParameters parameters)
    {
        var contents =
            await client.Repository.Content.GetRawContent(parameters.Name, parameters.Owner, parameters.Path);
        var rawContent = Encoding.UTF8.GetString(contents);

        return rawContent;
    }
}