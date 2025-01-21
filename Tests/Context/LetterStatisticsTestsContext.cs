using Shouldly;
using StatisticsCollectorApp.Models;

namespace Tests.Context;

public class LetterStatisticsTestsContext : BaseTestsContext
{
    private readonly LetterStatistics letterStatistics = new();

    public LetterStatisticsTestsContext ShouldHaveValue(char key, long value)
    {
        Assertions.Add(() => letterStatistics.GetStatistics().First(x => x.Key == key).Value.ShouldBe(value));

        return this;
    }

    public LetterStatisticsTestsContext ShouldReturnAllKeys(ICollection<char> keys)
    {
        Assertions.Add(() => keys.Count.ShouldBe(26));
        Assertions.Add(() => keys.ShouldContain('a'));
        Assertions.Add(() => keys.ShouldContain('z'));

        return this;
    }

    public LetterStatisticsTestsContext ShouldReturnOrderedStatistics(List<KeyValuePair<char, long>> statistics)
    {
        Assertions.Add(() => statistics.First().Key.ShouldBe('a'));
        Assertions.Add(() => statistics.First().Value.ShouldBe(5));
        Assertions.Add(() => statistics[1].Key.ShouldBe('b'));
        Assertions.Add(() => statistics[1].Value.ShouldBe(3));
        Assertions.Add(() => statistics.Skip(2).All(s => s.Value == default).ShouldBeTrue());

        return this;
    }

    public LetterStatistics CreateSubject()
    {
        return letterStatistics;
    }
}