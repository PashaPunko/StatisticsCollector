namespace StatisticsCollectorApp.Models;

public class RepositoryParameters
{
    public required string Name { get; set; }
    public required string Owner { get; set; }
    public string Path { get; set; } = string.Empty;

    public static RepositoryParameters Create(string owner, string name, string path)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(owner))
        {
            throw new ArgumentException("Name and Owner parameters must not be empty");
        }

        return new RepositoryParameters
        {
            Name = name,
            Owner = owner,
            Path = path
        };
    }

    public RepositoryParameters AddPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("Path parameter must not be empty");
        }

        return new RepositoryParameters
        {
            Name = Name,
            Owner = Owner,
            Path = path
        };
    }
}