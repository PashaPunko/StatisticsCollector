using StatisticsCollectorApp.Services;
using Tests.Context;

namespace Tests;

public class GitHubContentServiceTests
{
    private GitHubContentServiceTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new GitHubContentServiceTestsContext().Setup();
    }

    [Test]
    public async Task GetAllFilePathsAsync_WithTreeResponse_ReturnsContent()
    {
        context.SetupTreeResponseContents();

        var results = await context.CreateSubject<GitHubContentService>().GetAllFilePathsAsync(context.parameters);

        context.ValidateGetAllFilePathsAsync(results).Assert();
    }

    [Test]
    public async Task GetAllFilePathsAsync_WithRecursiveResponse_ReturnsContent()
    {
        context.SetupRepositoryContents();

        var results = await context.CreateSubject<GitHubContentService>().GetAllFilePathsAsync(context.parameters);

        context.ValidateGetAllFilePathsAsync(results).Assert();
    }

    [Test]
    public async Task GetRawContentAsync_WithValidParameters_ReturnsRawContent()
    {
        context.SetupRawContent();

        var results = await context.CreateSubject<GitHubContentService>().GetRawContentAsync(context.parameters);

        context.ValidateGetRawContentAsync(results).Assert();
    }
}