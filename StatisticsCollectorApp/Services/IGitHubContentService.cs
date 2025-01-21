using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IGitHubContentService
{
    Task<IEnumerable<GitHubRepositoryNodeInfo>> GetAllContentByPathAsync(RepositoryParameters parameters);
    Task<string> GetRawContentAsync(RepositoryParameters parameters);
}