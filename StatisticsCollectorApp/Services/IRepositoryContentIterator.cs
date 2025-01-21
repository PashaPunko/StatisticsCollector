using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IRepositoryContentIterator
{
    IAsyncEnumerable<string> IterateAsync(RepositoryParameters parameters,
        Func<GitHubRepositoryNodeInfo, bool> fileFilter);
}