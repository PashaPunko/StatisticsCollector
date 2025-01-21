using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IStatisticsCollector
{
    void CollectStatistics(LetterStatistics statistics, string fileContent);
}