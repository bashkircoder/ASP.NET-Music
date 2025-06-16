using System.Text;
using Microsoft.Extensions.Options;
using Music.Logger.Interfaces;
using Music.Logger.Settings;

namespace Music.Logger;

public class LogToFile(IOptions<FileName> options) : IMusicLogger
{
    public void WriteLog(string message)
    {
        var log = new StringBuilder();
        var timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        log.Append(string.Join(' ', timeStamp, message));
        
        var file = new StreamWriter(options.Value.LoggerFileName, true);
        file.WriteLine(log.ToString());
        file.Close();
        
    }
}