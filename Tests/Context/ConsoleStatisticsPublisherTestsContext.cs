using Shouldly;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class ConsoleStatisticsPublisherTestsContext : BaseTestsContext
{
    public LetterStatistics statistics;
    private StringWriter stringWriter;

    public ConsoleStatisticsPublisherTestsContext Setup()
    {
        stringWriter = Mocker.Create<StringWriter>();
        Console.SetOut(stringWriter);

        return this;
    }

    public ConsoleStatisticsPublisherTestsContext SetupStatistics()
    {
        statistics = new LetterStatistics();
        statistics.IncreaseValue('a', 5);
        statistics.IncreaseValue('b', 3);

        return this;
    }

    public ConsoleStatisticsPublisher GetSubject()
    {
        return Mocker.Create<ConsoleStatisticsPublisher>();
    }

    public ConsoleStatisticsPublisherTestsContext ValidateOutput()
    {
        Assertions.Add(() => stringWriter.ToString().ShouldContain("a: 5"));
        Assertions.Add(() => stringWriter.ToString().ShouldContain("b: 3"));
        Assertions.Add(() => stringWriter.ToString().ShouldContain("c: 0"));

        return this;
    }
}