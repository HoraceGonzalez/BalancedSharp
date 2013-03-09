using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// You can configure events to be published via a POST 
    /// to the endpoint of your choice via callbacks. Once configured,
    /// events are accessible via the events endpoint.
    /// </summary>
    public interface IEventClient
    {
        /// <summary>
        /// Retrieves the details of an event that was previously created.
        /// Use the uri that was previously returned.
        /// </summary>
        /// <param name="eventsUri">The event id.</param>
        /// <returns>Corresponding event information</returns>
        Status<Event> Get(string eventsUri);

        /// <summary>
        /// Retrieves a list off all events.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of event details</returns>
        Status<PagedList<Event>> List(string eventUri, int limit = 10, int offset = 0);
    }

    public class EventClient : IEventClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public EventClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Event> Get(string eventUri)
        {
            return rest.GetResult<Event>(eventUri, this.Service.Key, "", "get", null);
        }

        public Status<PagedList<Event>> List(string eventUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Event>>(eventUri, this.Service.Key, "", "get", parameters);
        }
    }
}
