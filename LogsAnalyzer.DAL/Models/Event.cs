using LiteDB;

namespace LogsAnalyzer.DAL
{
    public class Event
    {
        [BsonId(true)]
        public int Id { get; set; }

        public string EventId { get; set; }
        public double Duration { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }
        public bool Alert { get; set; }

        public Event(string id, double duration, string type, string host, bool alert)
        {
            EventId = id;
            Duration = duration;
            Type = type;
            Host = host;
            Alert = alert;
        }
    }
}