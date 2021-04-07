using LogsAnalyzer.BLL.Helpers;
using LogsAnalyzer.BLL.Models;
using Serilog;
using System.Collections.Generic;
using System.IO;

namespace LogsAnalyzer.BLL.Services
{
    public class LogsFileParser : ILogsFileParser
    {
        public IList<Event> Parse(string filePath)
        {
            var events = new List<Event>();
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Log.Information($"Processing file {filePath}");
                events = SerializationHelper.Deserialize<List<Event>>(file);
            }

            return events;
        }
    }
}