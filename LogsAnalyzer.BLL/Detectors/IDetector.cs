using LogsAnalyzer.BLL.Models;
using System.Collections.Generic;

namespace LogsAnalyzer.BLL.Detectors
{
    public interface IDetector
    {
        IList<Event> Detect(IList<Event> events);
    }
}