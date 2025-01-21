using Octokit;
using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class GitHubRepositoryAnalyzer(
    IStatisticsCollector statisticsCollector,
    IRepositoryContentIterator contentIterator) : IGitHubRepositoryAnalyzer
{
    private static readonly List<string> FileExtensionsToAnalyze = [".js", ".ts", ".tsx"];

    public async Task<LetterStatistics> CollectStatisticsAsync(RepositoryParameters parameters)
    {
        var letterStatistics = new LetterStatistics();
        await foreach (var file in
                       contentIterator.IterateAsync(parameters,
                           content => content.Type == ContentType.File &&
                                      FileExtensionsToAnalyze.Any(e => content.Name.EndsWith(e))))
        {
            statisticsCollector.CollectStatistics(letterStatistics, file);
        }

        return letterStatistics;
    }
}