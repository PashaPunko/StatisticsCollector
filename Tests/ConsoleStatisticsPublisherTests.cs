using StatisticsCollectorApp.Models;
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
        
        context.GetSubject().PublishStatistics(context.statistics);

        context.ValidateOutput().Assert();
    }
}