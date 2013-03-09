using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IMerchantClient : IBalancedServiceObject
    {
        Status<Merchant> Get(string merchantUri);

        Status<PagedList<Merchant>> List(int limit = 10, int offset = 0);
    }

    public class MerchantClient : IMerchantClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public MerchantClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Merchant> Get(string merchantUri)
        {
            return null;
        }

        public Status<PagedList<Merchant>> List(int limit = 10, int offset = 0)
        {
            return null;
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
