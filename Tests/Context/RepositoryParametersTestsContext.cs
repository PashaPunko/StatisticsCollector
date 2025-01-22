using Shouldly;
using StatisticsCollectorApp.Models;
using Guid = System.Guid;

namespace Tests.Context;

public class RepositoryParametersTestsContext : BaseTestsContext
{
    private string name;
    public string newPath = "newPath";
    private string owner;
    private string path;
    private string reference;
    private RepositoryParameters repositoryParameters;

    public RepositoryParametersTestsContext WithEmptyOwner()
    {
        owner = string.Empty;
        return this;
    }

    public RepositoryParametersTestsContext WithOwner()
    {
        owner = Guid.NewGuid().ToString();
        return this;
    }

    public RepositoryParametersTestsContext WithName()
    {
        name = Guid.NewGuid().ToString();
        return this;
    }

    public RepositoryParametersTestsContext WithEmptyName()
    {
        name = string.Empty;
        return this;
    }

    public RepositoryParametersTestsContext WithPath()
    {
        path = Guid.NewGuid().ToString();
        return this;
    }

    public RepositoryParametersTestsContext WithEmptyReference()
    {
        reference = string.Empty;
        return this;
    }

    public RepositoryParametersTestsContext WithReference()
    {
        reference = Guid.NewGuid().ToString();
        return this;
    }

    public RepositoryParameters CreateSubject()
    {
        repositoryParameters = RepositoryParameters.Create(owner, name, reference, path);
        return repositoryParameters;
    }

    public RepositoryParametersTestsContext ShouldHaveName()
    {
        Assertions.Add(() => repositoryParameters.Name.ShouldBe(name));
        return this;
    }

    public RepositoryParametersTestsContext ShouldHaveOwner()
    {
        Assertions.Add(() => repositoryParameters.Owner.ShouldBe(owner));
        return this;
    }

    public RepositoryParametersTestsContext ShouldHaveReference()
    {
        Assertions.Add(() => repositoryParameters.Reference.ShouldBe(reference));
        return this;
    }

    public RepositoryParametersTestsContext ShouldHavePath()
    {
        Assertions.Add(() => repositoryParameters.Path.ShouldBe(path));
        return this;
    }

    public RepositoryParametersTestsContext ShouldHaveNewPath(RepositoryParameters parameters)
    {
        Assertions.Add(() => parameters.Path.ShouldBe(newPath));
        return this;
    }
}