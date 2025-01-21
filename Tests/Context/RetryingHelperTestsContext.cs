using Shouldly;

namespace Tests.Context;

public class RetryingHelperTestsContext: BaseTestsContext
{
    public Func<Task<int>> Func { get; private set; }
    private int callCount;
    private const int ExpectedResult = 42;

    public RetryingHelperTestsContext SetupSuccessfulFunc()
    {
        Func = async () =>
        {
            callCount++;
            return await Task.FromResult(ExpectedResult);
        };
        return this;
    }

    public RetryingHelperTestsContext SetupFailingFunc()
    {
        Func = async () =>
        {
            callCount++;
            throw new Exception("Test exception");
        };
        return this;
    }

    public RetryingHelperTestsContext ValidateResult(int result)
    {
        Assertions.Add(() => result.ShouldBe(ExpectedResult));
        Assertions.Add(() => callCount.ShouldBe(1));

        return this;
    }

    public RetryingHelperTestsContext ValidateFailure()
    {
        Assertions.Add(() => callCount.ShouldBe(5));

        return this;
    }
}