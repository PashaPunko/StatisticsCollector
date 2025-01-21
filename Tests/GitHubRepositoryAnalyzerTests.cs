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
        context.SetupRepositoryParameters().SetupExpectedStatistics().SetupRepositoryContentIterator()
            .SetupLetterStatistics();

        var result = await context.GetSubject().CollectStatisticsAsync(context.repositoryParameters);

        context.ValidateCollectStatisticsAsync(result).Assert();
    }
}