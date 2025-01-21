namespace StatisticsCollectorApp.Models;

public class RepositoryParameters
{
    public required string Name { get; set; }
    public required string Owner { get; set; }
    public string Path { get; set; } = string.Empty;

    public string Reference { get; set; }

    public static RepositoryParameters Create(string owner, string name, string reference, string path)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(reference))
            throw new ArgumentException("Name, Owner and Reference parameters must not be empty");

        return new RepositoryParameters
        {
            Name = name,
            Owner = owner,
            Reference = reference,
            Path = path
        };
    }

    public RepositoryParameters AddPath(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path parameter must not be empty");

        return new RepositoryParameters
        {
            Name = Name,
            Owner = Owner,
            Reference = Reference,
            Path = path
        };
    }
}