using Moq;
using Octokit;
using Shouldly;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class RepositoryContentIteratorTestsContext : BaseTestsContext
{
    public RepositoryParameters parameters;
    private List<string> rawContent;

    public RepositoryContentIteratorTestsContext Setup()
    {
        parameters = RepositoryParameters.Create("owner", "repo", "path");
        return this;
    }

    public RepositoryContentIteratorTestsContext SetupAllContent()
    {
        var firstAllContent = new List<GitHubRepositoryNodeInfo>
        {
            new(ContentType.File, "path", "file1.js"),
            new(ContentType.Dir, "path", "dir")
        };
        var secondAllContent = new List<GitHubRepositoryNodeInfo>
        {
            new(ContentType.File, "path/dir", "file2.js")
        };

        Mocker.Mock<IGitHubContentService>()
            .SetupSequence(c => c.GetAllContentByPathAsync(It.IsAny<RepositoryParameters>()))
            .ReturnsAsync(firstAllContent)
            .ReturnsAsync(secondAllContent);
        return this;
    }

    public RepositoryContentIteratorTestsContext SetupRawContent()
    {
        rawContent = ["file1 raw content", "file2 raw content"];
        Mocker.Mock<IGitHubContentService>()
            .SetupSequence(c => c.GetRawContentAsync(It.IsAny<RepositoryParameters>()))
            .ReturnsAsync(rawContent.First())
            .ReturnsAsync(rawContent.Last());
        return this;
    }

    public RepositoryContentIterator GetSubject()
    {
        return Mocker.Create<RepositoryContentIterator>();
    }

    public RepositoryContentIteratorTestsContext ValidateIterateAsync(List<string> result)
    {
        result.ShouldNotBeNull();
        rawContent.ForEach(r => Assertions.Add(() => result.ShouldContain(r)));
        return this;
    }
}