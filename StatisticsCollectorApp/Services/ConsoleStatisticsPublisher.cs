using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public class ConsoleStatisticsPublisher : IStatisticsPublisher
{
    public void PublishStatistics(LetterStatistics statistics)
    {
        foreach (var (letter, count) in statistics.GetStatistics())
        {
            Console.WriteLine($"{letter}: {count}");
        }
    }
}