using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IEventClient
    {
        Status<Event> Get(string eventId);

        Status<PagedList<Event>> List(int limit = 10, int offset = 0);
    }

    public class EventClient : IEventClient
    {
        IBalancedService balanceService;

        public EventClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Event> Get(string eventId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Event>> List(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }
}
