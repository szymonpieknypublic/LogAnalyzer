using LogsAnalyzer.BLL.Models;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace LogsAnalyzer.BLL.Detectors
{
    public class MaxEventDurationDetector : IDetector
    {
        private const long MAX_EVENT_DURATION_IN_MILLISECONDS = 4;

        public IList<Event> Detect(IList<Event> events)
        {
            var detectedEvents = new List<Event>();
            var groups = events.GroupBy(x => x.Id).ToList();
            foreach (var group in groups)
            {
                var startedEvent = group.Where(c => c.State == EventStates.STARTED).FirstOrDefault();
                var finishedEvent = group.Where(c => c.State == EventStates.FINISHED).FirstOrDefault();
                if (startedEvent == null || finishedEvent == null)
                {
                    Log.Warning($"Event with [State]{EventStates.STARTED} or {EventStates.FINISHED} is missing for event with [Id:{group.Key}] ");
                    continue;
                }

                var duration = finishedEvent.TimestampValue - startedEvent.TimestampValue;
                var durationInMillisecond = duration.TotalMilliseconds;
                if (durationInMillisecond > MAX_EVENT_DURATION_IN_MILLISECONDS)
                {
                    finishedEvent.Alert = true;
                    finishedEvent.DurationInMilliseconds = durationInMillisecond;
                    detectedEvents.Add(finishedEvent);
                    Log.Warning($"Event [{nameof(finishedEvent.Id)}:{startedEvent.Id}] duration [{durationInMillisecond}ms] exceed {MAX_EVENT_DURATION_IN_MILLISECONDS}ms");
                }
                else
                {
                    Log.Information($"Event [{nameof(finishedEvent.Id)}:{startedEvent.Id}] duration [{durationInMillisecond}ms]");
                }
            }

            return detectedEvents;
        }
    }
}