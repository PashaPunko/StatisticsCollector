namespace StatisticsCollectorApp.Helpers;

public static class RetryingHelper
{
    private const int MaxNumberOfRetries = 4;
    private const int DefaultDelayStep = 1000;

    public static async Task<T> RetryAsync<T>(
        Func<Task<T>> func,
        int maxRetries = MaxNumberOfRetries,
        int delayStep = DefaultDelayStep)
    {
        int retryNumber = default;

        while (retryNumber <= maxRetries)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured: {e.Message}. Retrying for the {retryNumber} time...");

                if (retryNumber == maxRetries)
                {
                    Console.WriteLine($"Error occured: {e.Message}");
                    Console.WriteLine($"Stack Trace: {e.StackTrace}");

                    throw new InvalidOperationException(e.Message);
                }

                await Task.Delay(delayStep * retryNumber);
            }


            retryNumber++;
        }

        return default;
    }
}