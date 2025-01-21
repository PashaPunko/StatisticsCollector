using Moq;
using Shouldly;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class GitHubRepositoryAnalyzerTestsContext : BaseTestsContext
{
    private GitHubRepositoryAnalyzer analyzer;
    private IAsyncEnumerable<string> contents;
    private Dictionary<char, long> expectedStatistics;
    public RepositoryParameters repositoryParameters;

    public GitHubRepositoryAnalyzerTestsContext SetupExpectedStatistics()
    {
        expectedStatistics = new Dictionary<char, long> { { 'a', 2 }, { 'b', 2 }, { 'c', 1 }, { 'd', 1 } };

        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupRepositoryParameters()
    {
        repositoryParameters = RepositoryParameters.Create("owner", "repo", "path");

        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupRepositoryContentIterator()
    {
        contents = new List<string> { "abc", "abd" }.ToAsyncEnumerable();
        Mocker.Mock<IRepositoryContentIterator>()
            .Setup(i => i.IterateAsync(It.IsAny<RepositoryParameters>(),
                It.IsAny<Func<GitHubRepositoryNodeInfo, bool>>()))
            .Returns(contents).Verifiable();
        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupLetterStatistics()
    {
        Mocker.Mock<IStatisticsCollector>()
            .Setup(c => c.CollectStatistics(It.IsAny<LetterStatistics>(), It.IsAny<string>()))
            .Callback<LetterStatistics, string>((stats, content) =>
            {
                foreach (var c in content) stats.IncreaseValue(c, 1);
            }).Verifiable();
        return this;
    }

    public GitHubRepositoryAnalyzer GetSubject()
    {
        return Mocker.Create<GitHubRepositoryAnalyzer>();
    }

    public GitHubRepositoryAnalyzerTestsContext ValidateCollectStatisticsAsync(LetterStatistics actualStatistics)
    {
        Assertions.Add(() => actualStatistics.ShouldNotBeNull());
        foreach (var (key, value) in actualStatistics.GetStatistics())
            Assertions.Add(() => value.ShouldBe(expectedStatistics.GetValueOrDefault(key)));

        return this;
    }
}