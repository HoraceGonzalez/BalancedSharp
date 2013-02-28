using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IHoldClient
    {
        Status<Hold> New(string marketplaceId, string accountId, int amount, string accountUri = null,
            string appearsOnStatementAs = null, string description = null, Dictionary<string, string> meta = null,
            string sourceUri = null, string cardUri = null);

        Status<Hold> Get(string marketplaceId, string holdId);

        Status<PagedList<Hold>> List(string marketplaceId, int limit = 10, int offset = 0);

        Status<PagedList<Hold>> List(string marketplaceId, string accountId, int limit = 10, int offset = 0);

        Status<Hold> Update(string marketplaceId, string holdId, string description = null,
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null);

        Status<Hold> Capture(string marketplaceId, string accountId, string holdId);

        Status<Hold> Void(string marketplaceId, string holdId);
    }

    public class HoldClient : IHoldClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public HoldClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Hold> New(string marketplaceId, string accountId, int amount, 
            string accountUri = null, string appearsOnStatementAs = null, 
            string description = null, Dictionary<string, string> meta = null, string sourceUri = null, string cardUri = null)
        {
            throw new NotImplementedException();
        }

        public Status<Hold> Get(string marketplaceId, string holdId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Hold>> List(string marketplaceId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Hold>> List(string marketplaceId, string accountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Hold> Update(string marketplaceId, string holdId, string description = null, 
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null)
        {
            throw new NotImplementedException();
        }

        public Status<Hold> Capture(string marketplaceId, string accountId, string holdId)
        {
            throw new NotImplementedException();
        }

        public Status<Hold> Void(string marketplaceId, string holdId)
        {
            throw new NotImplementedException();
        }
    }
}
