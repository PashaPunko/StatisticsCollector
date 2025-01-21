using StatisticsCollectorApp.Helpers;
using Tests.Context;

namespace Tests;

public class RetryingHelperTests
{
    private RetryingHelperTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new RetryingHelperTestsContext();
    }

    [Test]
    public async Task RetryAsync_WithSuccessfulExecution_ReturnsResult()
    {
        context.SetupSuccessfulFunc();

        var result = await RetryingHelper.RetryAsync(context.Func);

        context.ValidateResult(result).Assert();
    }

    [Test]
    public void RetryAsync_WithMaxRetriesExceeded_ThrowsInvalidOperationException()
    {
        context.SetupFailingFunc();

        context.ShouldThrow<InvalidOperationException>(async () => await RetryingHelper.RetryAsync(context.Func));
        context.ValidateFailure().Assert();
    }
}