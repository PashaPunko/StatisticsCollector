using Shouldly;
using StatisticsCollectorApp.Models;
using StatisticsCollectorApp.Services;

namespace Tests.Context;

public class LetterStatisticsCollectorTestsContext : BaseTestsContext
{
    private Dictionary<char, long> expectedStatistics;
    public string fileContent;
    public LetterStatistics statistics = new();

    public LetterStatisticsCollectorTestsContext SetupFileContent()
    {
        fileContent = "abcabd";
        return this;
    }

    public LetterStatisticsCollectorTestsContext SetupExpectedStatistics()
    {
        expectedStatistics = new Dictionary<char, long>
        {
            { 'a', 2 },
            { 'b', 2 },
            { 'c', 1 },
            { 'd', 1 }
        };

        return this;
    }

    public LetterStatisticsCollector GetSubject()
    {
        return Mocker.Create<LetterStatisticsCollector>();
    }

    public LetterStatisticsCollectorTestsContext ValidateCollectStatistics()
    {
        foreach (var (key, value) in statistics.GetStatistics())
            Assertions.Add(() => value.ShouldBe(expectedStatistics.GetValueOrDefault(key)));

        return this;
    }
}