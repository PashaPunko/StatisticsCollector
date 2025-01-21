using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IGitHubContentService
{
    Task<List<string>> GetAllFilePathsAsync(RepositoryParameters parameters);
    Task<string> GetRawContentAsync(RepositoryParameters parameters);
}