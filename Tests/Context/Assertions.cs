namespace Tests.Context;

public class Assertions
{
    private readonly List<Action> assertions = [];

    public void Add(Action assertAction)
    {
        assertions.Add(assertAction);
    }

    public void Assert()
    {
        var messagesList = new List<string>();

        assertions.ForEach(assertion =>
        {
            try
            {
                assertion();
            }
            catch (Exception e)
            {
                messagesList.Add(e.Message);
            }
        });

        if (!messagesList.Any())
        {
            return;
        }
        var message = $"\n{string.Join("\n", messagesList)}";

        NUnit.Framework.Assert.Fail(message);
    }
}