using Microsoft.Extensions.Options;
using Octokit;
using Shouldly;
using StatisticsCollectorApp.Options;

namespace Tests.Context;

public class GitHubClientFactoryTestsContext : BaseTestsContext
{
    private readonly string token = Guid.NewGuid().ToString();

    public GitHubClientFactoryTestsContext SetupConfigurationWithValidToken()
    {
        Mocker.Mock<IOptions<GitHubOptions>>().Setup(x => x.Value)
            .Returns(new GitHubOptions { Token = token }).Verifiable();

        return this;
    }

    public GitHubClientFactoryTestsContext SetupConfigurationWithInvalidToken()
    {
        Mocker.Mock<IOptions<GitHubOptions>>().Setup(x => x.Value)
            .Returns(new GitHubOptions { Token = string.Empty }).Verifiable();

        return this;
    }

    public GitHubClientFactoryTestsContext ShouldReturnCredentialsWithToken(GitHubClient client)
    {
        client.Credentials.GetToken().ShouldBe(token);

        return this;
    }

    public GitHubClientFactoryTestsContext ShouldReturnAnonymousCredentials(GitHubClient client)
    {
        client.Credentials.ShouldBe(Credentials.Anonymous);

        return this;
    }
}