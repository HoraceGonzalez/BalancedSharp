using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    ///You can configure events to be published via a POST 
    ///to the endpoint of your choice via callbacks. Once configured,
    ///events are accessible via the events endpoint.
    /// </summary>
    public interface IEventClient
    {
        /// <summary>
        /// Retrieves the details of an event that was previously created.
        /// Use the uri that was previously returned.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Corresponding event information</returns>
        Status<Event> Get(string eventId);

        /// <summary>
        /// Get a list off all events.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>Event List.</returns>
        Status<PagedList<Event>> List(int limit = 10, int offset = 0);
    }

    public class EventClient : IEventClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public EventClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Event> Get(string eventId)
        {
            string url = string.Format("{0}{1}/events/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, eventId);

            return rest.GetResult<Event>(url, this.balanceService.Key, "", "get", null);
        }

        public Status<PagedList<Event>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/events?{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, limit);
            
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Event>>(url, this.balanceService.Key, "", "get", parameters);
        }
    }
}
