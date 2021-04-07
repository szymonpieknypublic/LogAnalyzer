using Newtonsoft.Json;
using System;

namespace LogsAnalyzer.BLL.Models
{
    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        public TimeSpan TimestampValue => TimeSpan.FromMilliseconds(Timestamp);

        public bool Alert { get; set; }
        public double DurationInMilliseconds { get; set; }
    }
}