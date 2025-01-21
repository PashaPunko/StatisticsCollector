using Moq;
using Shouldly;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class GitHubRepositoryAnalyzerTestsContext : BaseTestsContext
{
    private List<string> files;
    private Dictionary<char, long> expectedStatistics;
    public RepositoryParameters repositoryParameters;

    public GitHubRepositoryAnalyzerTestsContext SetupExpectedStatistics()
    {
        expectedStatistics = new Dictionary<char, long> { { 'a', 2 }, { 'b', 2 }, { 'c', 1 }, { 'd', 1 } };

        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupRepositoryParameters()
    {
        repositoryParameters = RepositoryParameters.Create("owner", "repo", "reference", "path");

        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupFilePaths()
    {
        files = ["abc.js", "abd.ts"];
        Mocker.Mock<IGitHubContentService>()
            .Setup(i => i.GetAllFilePathsAsync(It.IsAny<RepositoryParameters>()))
            .ReturnsAsync(files).Verifiable();
        return this;
    }

    public GitHubRepositoryAnalyzerTestsContext SetupContent()
    {
        Mocker.Mock<IGitHubContentService>()
            .Setup(i => i.GetRawContentAsync(It.Is<RepositoryParameters>(p => p.Path == files[0])))
            .ReturnsAsync("abc").Verifiable();
        Mocker.Mock<IGitHubContentService>()
            .Setup(i => i.GetRawContentAsync(It.Is<RepositoryParameters>(p => p.Path == files[1])))
            .ReturnsAsync("abd").Verifiable();
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

    public GitHubRepositoryAnalyzerTestsContext ValidateCollectStatisticsAsync(LetterStatistics actualStatistics)
    {
        Assertions.Add(() => actualStatistics.ShouldNotBeNull());
        foreach (var (key, value) in actualStatistics.GetStatistics())
            Assertions.Add(() => value.ShouldBe(expectedStatistics.GetValueOrDefault(key)));

        return this;
    }
}