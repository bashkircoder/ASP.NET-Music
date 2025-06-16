namespace Music.Logger;

public class LogToConsole
{
    public async Task WriteLog(string message)
    {
       Console.WriteLine(message);
    }
}