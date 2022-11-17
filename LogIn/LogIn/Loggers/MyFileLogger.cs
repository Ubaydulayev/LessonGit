namespace LogIn.Loggers;

public class MyFileLogger : ILogger
{
	public IDisposable BeginScope<TState>(TState state)
	{
		return null;
	}
	public bool IsEnabled(LogLevel logLevel)
	{
		return true;
	}

    public void Log<TState>(
    LogLevel logLevel,
    EventId eventId,
    TState state,
    Exception exception,
    Func<TState, Exception, string> formatter)
    {
        var message = formatter(state, exception);
        var str = File.ReadAllLines("log.txt").ToList();
        str.Add("MyLogger: " + message);
        System.IO.File.WriteAllLines("log.txt", str);
    }
}