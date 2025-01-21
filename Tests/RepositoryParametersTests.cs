using Tests.Context;

namespace Tests;

public class RepositoryParametersTests
{
    private RepositoryParametersTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new RepositoryParametersTestsContext();
    }

    [Test]
    public void Create_WithValidParameters_CreatesRepositoryParameters()
    {
        context.WithOwner().WithName().WithPath();

        context.CreateSubject();

        context.ShouldHaveOwner()
            .ShouldHaveName()
            .ShouldHavePath()
            .Assert();
    }

    [Test]
    public void Create_WithEmptyName_ThrowsArgumentException()
    {
        context.WithOwner().WithEmptyName().WithPath();

        context.ShouldThrow<ArgumentException>(() => context.CreateSubject()).Assert();
    }

    [Test]
    public void Create_WithEmptyOwner_ThrowsArgumentException()
    {
        context.WithName().WithEmptyOwner().WithPath();

        context.ShouldThrow<ArgumentException>(() => context.CreateSubject()).Assert();
    }

    [Test]
    public void AddPath_WithValidPath_UpdatesPath()
    {
        context.WithOwner().WithName();

        var subject = context.CreateSubject();
        var newSubject = subject.AddPath(context.newPath);

        context.ShouldHaveOwner()
            .ShouldHaveName()
            .ShouldHaveNewPath(newSubject)
            .Assert();
    }

    [Test]
    public void AddPath_WithEmptyPath_ThrowsArgumentException()
    {
        context.WithOwner().WithName();

        var subject = context.CreateSubject();

        context.ShouldThrow<ArgumentException>(() => subject.AddPath(string.Empty)).Assert();
    }
}