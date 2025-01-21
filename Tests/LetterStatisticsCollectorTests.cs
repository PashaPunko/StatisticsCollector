using Tests.Context;

namespace Tests;

public class LetterStatisticsCollectorTests
{
    private LetterStatisticsCollectorTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new LetterStatisticsCollectorTestsContext();
    }

    [Test]
    public void CollectStatistics_WithValidContent_UpdatesStatistics()
    {
        context.SetupFileContent().SetupExpectedStatistics();

        context.GetSubject().CollectStatistics(context.statistics, context.fileContent);

        context.ValidateCollectStatistics().Assert();
    }
}