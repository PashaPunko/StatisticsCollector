using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IGitHubRepositoryAnalyzer
{
    Task<LetterStatistics> CollectStatisticsAsync(RepositoryParameters parameters);
}