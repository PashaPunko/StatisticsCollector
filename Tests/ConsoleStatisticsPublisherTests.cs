using StatisticsCollectorApp.Services;
using Tests.Context;

namespace Tests;

public class ConsoleStatisticsPublisherTests
{
    private ConsoleStatisticsPublisherTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new ConsoleStatisticsPublisherTestsContext().Setup();
    }

    [Test]
    public void PublishStatistics_WithValidStatistics_PrintsStatistics()
    {
        context.SetupStatistics();

        context.CreateSubject<ConsoleStatisticsPublisher>().PublishStatistics(context.statistics);

        context.ValidateOutput().Assert();
    }
}