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
    private IEnumerable<RepositoryContent> contents;
    private readonly string name = Guid.NewGuid().ToString();
    private readonly string owner = Guid.NewGuid().ToString();
    public RepositoryParameters parameters;
    private readonly string path = Guid.NewGuid().ToString();
    private byte[] rawContent;
    private Mock<IRepositoryContentsClient> repositoryContentsClientMock;
    private GitHubContentService service;

    public GitHubContentServiceTestsContext Setup()
    {
        parameters = RepositoryParameters.Create(owner, name, path);
        var clientFactoryMock = Mocker.Mock<IGitHubClientFactory>();
        var clientMock = Mocker.Mock<IGitHubClient>();
        repositoryContentsClientMock = Mocker.Mock<IRepositoryContentsClient>();

        clientMock.Setup(c => c.Repository.Content).Returns(repositoryContentsClientMock.Object);
        clientFactoryMock.Setup(f => f.GetOrCreate()).Returns(clientMock.Object);

        service = new GitHubContentService(clientFactoryMock.Object);
        return this;
    }

    public GitHubContentServiceTestsContext SetupRepositoryContents()
    {
        contents = new List<RepositoryContent>
        {
            new("file.js", "path", string.Empty, default, ContentType.File, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
            new("dir", "path", string.Empty, default, ContentType.Dir, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
        };
        repositoryContentsClientMock
            .Setup(c => c.GetAllContents(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(contents.ToList());
        return this;
    }

    public GitHubContentServiceTestsContext SetupRawContent()
    {
        rawContent = "raw content"u8.ToArray();
        ;
        repositoryContentsClientMock
            .Setup(c => c.GetRawContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(rawContent);
        return this;
    }

    public GitHubContentService GetSubject()
    {
        return service;
    }

    public GitHubContentServiceTestsContext ValidateGetAllContentByPathAsync(
        IEnumerable<GitHubRepositoryNodeInfo> result)
    {
        Assertions.Add(() => result.ShouldNotBeNull());
        foreach (var content in contents)
            Assertions.Add(() =>
                result.ShouldContain(x => x.Name == content.Name && x.Path == content.Path && x.Path == content.Path));

        return this;
    }

    public GitHubContentServiceTestsContext ValidateGetRawContentAsync(string result)
    {
        Assertions.Add(() => result.ShouldBe(Encoding.UTF8.GetString(rawContent)));

        return this;
    }
}