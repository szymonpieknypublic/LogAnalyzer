using LogsAnalyzer.BLL.Models;
using System.Collections.Generic;

namespace LogsAnalyzer.BLL.Services
{
    public interface ILogsFileParser
    {
        IList<Event> Parse(string filePath);
    }
}