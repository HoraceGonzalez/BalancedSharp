using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IMarketplaceClient : IBalancedServiceObject
    {
        Status<PagedList<Marketplace>> List(int limit = 10, int offset = 0);
    }

    public class MarketplaceClient : IMarketplaceClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public MarketplaceClient(IBalancedService balanceService, IBalancedRest rest)
        {
            Service = balanceService;
            this.rest = rest;
        }

        public Status<PagedList<Marketplace>> List(int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Marketplace>>(this.Service.BaseUrl + "/v1/marketplaces", this.Service.Key, null, "get", parameters).AttachService(Service);
        }
    }
}
