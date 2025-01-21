using Octokit;
using StatisticsCollectorApp.Factories;
using Tests.Context;

namespace Tests;

public class GitHubClientFactoryTests
{
    private GitHubClientFactoryTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new GitHubClientFactoryTestsContext();
    }

    [Test]
    public void CreateClient_WithValidConfiguration_ReturnsGitHubClient()
    {
        var subject = context.SetupConfigurationWithValidToken().CreateSubject<GitHubClientFactory>();

        var client = subject.GetOrCreate();

        context.ShouldReturnCredentialsWithToken((GitHubClient)client).Assert();
    }

    [Test]
    public void CreateClient_WithInvalidConfiguration_ThrowsException()
    {
        var subject = context.SetupConfigurationWithInvalidToken().CreateSubject<GitHubClientFactory>();

        var client = subject.GetOrCreate();

        context.ShouldReturnAnonymousCredentials((GitHubClient)client).Assert();
    }
}