using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class GitHubRepositoryAnalyzer(
    IStatisticsCollector statisticsCollector,
    IGitHubContentService contentService) : IGitHubRepositoryAnalyzer
{
    private static readonly List<string> FileExtensionsToAnalyze = [".js", ".ts", ".tsx"];

    public async Task<LetterStatistics> CollectStatisticsAsync(RepositoryParameters parameters)
    {
        try
        {
            var letterStatistics = new LetterStatistics();
            var files = await contentService.GetAllFilePathsAsync(parameters);
            foreach (var file in files.Where(filePath => FileExtensionsToAnalyze.Any(filePath.EndsWith)))
            {
                var fileParameters = parameters.AddPath(file);
                var fileContent = await contentService.GetRawContentAsync(fileParameters);
                statisticsCollector.CollectStatistics(letterStatistics, fileContent);
            }

            return letterStatistics;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Statistic collection failed due to error: " + ex.Message);
            Console.WriteLine($"Initial repository parameters: name: {parameters.Name}, owner: {parameters.Owner}, reference: {parameters.Reference}");
            Console.WriteLine("Stack Trace: " + ex.StackTrace);
            throw new InvalidOperationException("Statistic collection failed", ex);
        }
    }
}