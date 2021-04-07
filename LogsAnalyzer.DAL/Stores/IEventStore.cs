using System.Collections.Generic;

namespace LogsAnalyzer.DAL.Stores
{
    public interface IEventStore
    {
        void Insert(IList<Event> @events);
    }
}