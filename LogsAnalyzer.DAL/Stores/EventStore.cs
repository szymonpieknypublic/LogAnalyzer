using LiteDB;
using System;
using System.Collections.Generic;

namespace LogsAnalyzer.DAL.Stores
{
    public class EventStore : IEventStore
    {
        private const string DATABASE_NAME = "LogsAnalyzer.db";

        public void Insert(IList<Event> @events)
        {
            using (var db = new LiteDatabase($@"{AppDomain.CurrentDomain.BaseDirectory}\{DATABASE_NAME}"))
            {
                var eventTable = db.GetCollection<Event>("event");
                eventTable.InsertBulk(events);
            }
        }
    }
}