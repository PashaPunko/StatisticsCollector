using Octokit;
using Tests.Context;

namespace Tests;

public class RepositoryContentIteratorTests
{
    private RepositoryContentIteratorTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new RepositoryContentIteratorTestsContext().Setup();
    }

    [Test]
    public async Task IterateAsync_WithValidParameters_ReturnsRawContent()
    {
        context.SetupAllContent().SetupRawContent();

        var result = await context.GetSubject()
            .IterateAsync(context.parameters, content => content.Type == ContentType.File).ToListAsync();

        context.ValidateIterateAsync(result).Assert();
    }
}