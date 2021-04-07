using LogsAnalyzer.BLL.Detectors;
using LogsAnalyzer.DAL.Stores;
using System.Linq;

namespace LogsAnalyzer.BLL.Services
{
    public class LogsAnalyzer : ILogsAnalyzer
    {
        private IDetector _detector;
        private ILogsFileParser _logsFileParser;
        private IEventStore _eventStore;

        public LogsAnalyzer(IDetector detector, ILogsFileParser logsFileParser, IEventStore eventStore)
        {
            _detector = detector;
            _logsFileParser = logsFileParser;
            _eventStore = eventStore;
        }

        public void Analyze(string logsFilePath)
        {
            var events = _logsFileParser.Parse(logsFilePath);
            var detectedEvents = _detector.Detect(events);
            _eventStore.Insert(detectedEvents.Select(c => new DAL.Event(c.Id, c.DurationInMilliseconds, c.Type, c.Host, c.Alert)).ToList());
        }
    }
}