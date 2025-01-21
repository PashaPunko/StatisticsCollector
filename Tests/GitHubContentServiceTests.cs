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
    public async Task GetAllContentByPathAsync_WithValidParameters_ReturnsContent()
    {
        context.SetupRepositoryContents();

        var results = await context.GetSubject().GetAllContentByPathAsync(context.parameters);

        context.ValidateGetAllContentByPathAsync(results).Assert();
    }

    [Test]
    public async Task GetRawContentAsync_WithValidParameters_ReturnsRawContent()
    {
        context.SetupRawContent();

        var results = await context.GetSubject().GetRawContentAsync(context.parameters);

        context.ValidateGetRawContentAsync(results).Assert();
    }
}