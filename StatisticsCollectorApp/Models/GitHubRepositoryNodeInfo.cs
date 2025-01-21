using Octokit;

namespace StatisticsCollectorApp.Models;

public record GitHubRepositoryNodeInfo(ContentType Type, string Path, string Name)
{
    public static GitHubRepositoryNodeInfo Create(RepositoryContent content)
    {
        return new GitHubRepositoryNodeInfo(content.Type.Value, content.Path, content.Name);
    }
}