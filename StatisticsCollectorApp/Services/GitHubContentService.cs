using System.Text;
using Octokit;
using StatisticsCollectorApp.Factories;
using StatisticsCollectorApp.Helpers;
using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class GitHubContentService(IGitHubClientFactory clientFactory) : IGitHubContentService
{
    private readonly IGitHubClient client = clientFactory.GetOrCreate();

    public async Task<List<string>> GetAllFilePathsAsync(RepositoryParameters parameters)
    {
        var response = await RetryingHelper.RetryAsync(() =>
            client.Git.Tree.GetRecursive(parameters.Owner, parameters.Name, parameters.Reference));

        if (response == null)
        {
            throw new InvalidOperationException("GitHub Api Tree Response is null");
        }

        if (response.Truncated)
        {
            return await GetAllFilePathsRecursivelyAsync(parameters).ToListAsync();
        }

        return response.Tree
            .Where(x => x.Type == TreeType.Blob)
            .Select(x => x.Path).ToList();
    }

    public async Task<string> GetRawContentAsync(RepositoryParameters parameters)
    {
        var contents =
            await RetryingHelper.RetryAsync(() =>
                client.Repository.Content.GetRawContent(parameters.Name, parameters.Owner, parameters.Path));
        var rawContent = Encoding.UTF8.GetString(contents);

        return rawContent;
    }

    private async IAsyncEnumerable<string> GetAllFilePathsRecursivelyAsync(RepositoryParameters parameters)
    {
        var dirContent = !parameters.Path.Any()
            ? await RetryingHelper.RetryAsync(() =>
                client.Repository.Content.GetAllContents(parameters.Name, parameters.Owner)) ?? []
            : await RetryingHelper.RetryAsync(() =>
                client.Repository.Content.GetAllContents(parameters.Name, parameters.Owner, parameters.Path)) ?? [];

        foreach (var file in dirContent.Where(content => content.Type == ContentType.File))
        {
            yield return file.Path;
        }

        foreach (var item in dirContent.Where(c => c.Type == ContentType.Dir))
        {
            var dirParameters = parameters.AddPath(item.Path);
            await foreach (var file in GetAllFilePathsRecursivelyAsync(dirParameters))
            {
                yield return file;
            }
        }
    }
}