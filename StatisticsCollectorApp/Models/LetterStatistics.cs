namespace StatisticsCollectorApp.Models;

public class LetterStatistics
{
    private const char FirstLetter = 'a';
    private const int NumberOfLetters = 26;

    private readonly IDictionary<char, long> letterStatistics = Enumerable.Range(default, NumberOfLetters)
        .ToDictionary(i => (char)(FirstLetter + i), _ => default(long));

    public ICollection<char> GetKeys()
    {
        return letterStatistics.Keys;
    }

    public List<KeyValuePair<char, long>> GetStatistics()
    {
        return letterStatistics.OrderByDescending(x => x.Value).ToList();
    }

    public void IncreaseValue(char key, long value)
    {
        if (!letterStatistics.ContainsKey(key)) return;

        letterStatistics[key] += value;
    }
}