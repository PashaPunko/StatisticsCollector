using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class LetterStatisticsCollector : IStatisticsCollector
{
    public void CollectStatistics(LetterStatistics statistics, string fileContent)
    {
        foreach (var letter in statistics.GetKeys())
        {
            var count = fileContent.Count(c => char.ToLower(c) == char.ToLower(letter));
            statistics.IncreaseValue(letter, count);
        }
    }
}