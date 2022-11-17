using System;
namespace LogIn.Loggers;
public class MyLoggerProvider : ILoggerProvider
{
	public void Dispose() { }
	public ILogger CreateLogger (string categoryName)
	{
		return new MyFileLogger();
	}
		
}