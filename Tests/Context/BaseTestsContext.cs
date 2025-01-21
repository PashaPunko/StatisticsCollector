using Autofac.Extras.Moq;
using Shouldly;

namespace Tests.Context;

public abstract class BaseTestsContext
{
    protected readonly Assertions Assertions = new();

    protected AutoMock Mocker { get; set; } = AutoMock.GetLoose();

    public void Assert()
    {
        ServicesShouldBeExecuted();
        Assertions.Assert();
    }

    public BaseTestsContext ServicesShouldBeExecuted()
    {
        Assertions.Add(Mocker.MockRepository.Verify);

        return this;
    }

    public TSubject CreateSubject<TSubject>()
        where TSubject : class
    {
        return Mocker.Create<TSubject>();
    }

    public BaseTestsContext ShouldThrow<TException>(Action actor)
        where TException : Exception
    {
        Assertions.Add(() => actor.ShouldThrow<TException>());

        return this;
    }

    public BaseTestsContext ShouldThrow<TException>(Func<Task> actor)
        where TException : Exception
    {
        Assertions.Add(() => actor.ShouldThrow<TException>());

        return this;
    }
}