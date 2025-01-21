using System.Text;
using Moq;
using Octokit;
using Shouldly;
using StatisticsCollectorApp.Factories;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class GitHubContentServiceTestsContext : BaseTestsContext
{
    private readonly string name = Guid.NewGuid().ToString();
    private readonly string owner = Guid.NewGuid().ToString();
    private readonly string path = Guid.NewGuid().ToString();
    private readonly string reference = Guid.NewGuid().ToString();
    private readonly List<string> filesToReturn = ["path/file1", "path/dir/file2"];
    public RepositoryParameters parameters;
    private byte[] rawContent;
    private Mock<IRepositoryContentsClient> repositoryContentsClientMock;
    private Mock<ITreesClient> treeClientMock;

    public GitHubContentServiceTestsContext Setup()
    {
        parameters = RepositoryParameters.Create(owner, name, reference, path);
        var clientFactoryMock = Mocker.Mock<IGitHubClientFactory>();
        var clientMock = Mocker.Mock<IGitHubClient>();
        repositoryContentsClientMock = Mocker.Mock<IRepositoryContentsClient>();
        treeClientMock = Mocker.Mock<ITreesClient>();

        clientMock.Setup(c => c.Repository.Content).Returns(repositoryContentsClientMock.Object);
        clientMock.Setup(c => c.Git.Tree).Returns(treeClientMock.Object);
        clientFactoryMock.Setup(f => f.GetOrCreate()).Returns(clientMock.Object);
        return this;
    }

    public GitHubContentServiceTestsContext SetupTreeResponseContents()
    {
        var treeItems = new List<TreeItem>
        {
            new(filesToReturn[0], "mode", TreeType.Blob, default, string.Empty, string.Empty),
            new("tree", "mode", TreeType.Tree, default, string.Empty, string.Empty),
            new("commit", "mode", TreeType.Commit, default, string.Empty, string.Empty),
            new(filesToReturn[1], "mode", TreeType.Blob, default, string.Empty, string.Empty)
        };
        var treeResponse = new TreeResponse(string.Empty, string.Empty, treeItems, false);
        treeClientMock
            .Setup(c => c.GetRecursive(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(treeResponse);
        return this;
    }

    public GitHubContentServiceTestsContext SetupRepositoryContents()
    {
        var treeResponse = new TreeResponse(string.Empty, string.Empty, [], true);
        treeClientMock
            .Setup(c => c.GetRecursive(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(treeResponse);

        var upperContents = new List<RepositoryContent>
        {
            new("file1", filesToReturn[0], string.Empty, default, ContentType.File, string.Empty, string.Empty,
                string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),

            new("dir", "path", string.Empty, default, ContentType.Dir, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        };
        var lowerContents = new List<RepositoryContent>
        {
            new("file2", filesToReturn[1], string.Empty, default, ContentType.File, string.Empty, string.Empty,
                string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        };

        repositoryContentsClientMock
            .SetupSequence(c => c.GetAllContents(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(upperContents)
            .ReturnsAsync(lowerContents);
        return this;
    }

    public GitHubContentServiceTestsContext SetupRawContent()
    {
        rawContent = "raw content"u8.ToArray();
        repositoryContentsClientMock
            .Setup(c => c.GetRawContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(rawContent);
        return this;
    }

    public GitHubContentServiceTestsContext ValidateGetAllFilePathsAsync(
        IEnumerable<string> result)
    {
        Assertions.Add(() => result.ShouldNotBeEmpty());
        foreach (var content in filesToReturn)
        {
            Assertions.Add(() => result.ShouldContain(x => x == content));
        }

        return this;
    }

    public GitHubContentServiceTestsContext ValidateGetRawContentAsync(string result)
    {
        Assertions.Add(() => result.ShouldBe(Encoding.UTF8.GetString(rawContent)));

        return this;
    }
}