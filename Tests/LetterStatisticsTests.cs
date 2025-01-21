using Tests.Context;

namespace Tests;

public class LetterStatisticsTests
{
    private LetterStatisticsTestsContext context;

    [SetUp]
    public void Setup()
    {
        context = new LetterStatisticsTestsContext();
    }

    [Test]
    public void IncreaseValue_WithValidKey_IncreasesValue()
    {
        var subject = context.CreateSubject();
        var key = 'a';
        var value = 5L;

        subject.IncreaseValue(key, value);

        context.ShouldHaveValue(key, value).Assert();
    }

    [Test]
    public void IncreaseValue_WithInvalidKey_ThrowsException()
    {
        var subject = context.CreateSubject();
        var key = 'z' + 1;

        context.ShouldThrow<ArgumentException>(() => subject.IncreaseValue((char)key, 1)).Assert();
    }

    [Test]
    public void GetKeys_ReturnsAllKeys()
    {
        var subject = context.CreateSubject();

        var keys = subject.GetKeys();

        context.ShouldReturnAllKeys(keys).Assert();
    }

    [Test]
    public void GetStatistics_ReturnsOrderedStatistics()
    {
        var subject = context.CreateSubject();
        subject.IncreaseValue('a', 5);
        subject.IncreaseValue('b', 3);

        var statistics = subject.GetStatistics();

        context.ShouldReturnOrderedStatistics(statistics).Assert();
    }
}