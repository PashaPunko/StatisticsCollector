using Octokit;
using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class RepositoryContentIterator(IGitHubContentService contentService) : IRepositoryContentIterator
{
    public async IAsyncEnumerable<string> IterateAsync(RepositoryParameters parameters,
        Func<GitHubRepositoryNodeInfo, bool> fileFilter)
    {
        var allContent = (await contentService.GetAllContentByPathAsync(parameters)).ToList();
        foreach (var file in allContent.Where(fileFilter))
        {
            var fileParameters = parameters.AddPath(file.Path);
            yield return await contentService.GetRawContentAsync(fileParameters);
        }

        foreach (var item in allContent.Where(c => c.Type == ContentType.Dir))
        {
            var dirParameters = parameters.AddPath(item.Path);
            await foreach (var file in IterateAsync(dirParameters, fileFilter))
            {
                yield return file;
            }
        }
    }
}