using StatisticsCollectorApp.Services;
using Tests.Context;

namespace Tests;

public class GitHubRepositoryAnalyzerTests
{
    private GitHubRepositoryAnalyzerTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new GitHubRepositoryAnalyzerTestsContext();
    }

    [Test]
    public async Task CollectStatisticsAsync_WithValidParameters_ReturnsStatistics()
    {
        context.SetupRepositoryParameters().SetupExpectedStatistics().SetupFilePaths().SetupContent()
            .SetupLetterStatistics();

        var result = await context.CreateSubject<GitHubRepositoryAnalyzer>().CollectStatisticsAsync(context.repositoryParameters);

        context.ValidateCollectStatisticsAsync(result).Assert();
    }
}