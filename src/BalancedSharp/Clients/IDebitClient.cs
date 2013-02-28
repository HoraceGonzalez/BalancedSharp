using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IDebitClient
    {
        Status<Debit> New(string marketplaceId, string accountId, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string accountUri = null, string onBehalfOfUri = null, string merchantUri = null,
            string holdUri = null, string sourceUri = null);

        Status<Debit> Get(string marketplaceId, string accountId, string debitId);

        Status<PagedList<Debit>> List(string marketplaceId, int limit = 10, int offset = 0);

        Status<PagedList<Debit>> List(string marketplaceId, string accountId, int limit = 10, int offset = 0);

        Status<Debit> Update(string marketplaceId, string accountId, string debitId,
            Dictionary<string, string> meta = null, string description = null);
    }

    public class DebitClient : IDebitClient
    {
        IBalancedService balanceService;

        public DebitClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Debit> New(string marketplaceId, string accountId, int? amount = null, 
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null, 
            string accountUri = null, string onBehalfOfUri = null, string merchantUri = null, 
            string holdUri = null, string sourceUri = null)
        {
            throw new NotImplementedException();
        }

        public Status<Debit> Get(string marketplaceId, string accountId, string debitId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Debit>> List(string marketplaceId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Debit>> List(string marketplaceId, string accountId, 
            int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Debit> Update(string marketplaceId, string accountId, 
            string debitId, Dictionary<string, string> meta = null, string description = null)
        {
            throw new NotImplementedException();
        }
    }
}
