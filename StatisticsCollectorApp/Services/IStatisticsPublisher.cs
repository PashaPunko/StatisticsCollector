using StatisticsCollectorApp.Models;

namespace StatisticsCollectorApp.Services;

public interface IStatisticsPublisher
{
    void PublishStatistics(LetterStatistics statistics);
}