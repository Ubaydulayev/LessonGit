namespace LogIn.Service;

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        var str = File.ReadAllLines("log.txt").ToList();
        str.Add(message);
        System.IO.File.WriteAllLines("log.txt", str);   
    }
}
