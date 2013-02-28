using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IRefundClient
    {
        Status<Refund> New(string marketplaceId, string debitId, int? amount = null, string description = null);

        Status<Refund> Get(string marketplaceId, string refundId);

        Status<PagedList<Refund>> List(string marketplaceId, int limit = 10, int offset = 0);

        Status<PagedList<Refund>> List(string marketplaceId, string accountId, int limit = 10, int offset = 0);

        Status<Refund> Update(string marketplaceId, string description = null, Dictionary<string, string> meta = null);
    }

    public class RefundClient : IRefundClient
    {
        IBalancedService balanceService;

        public RefundClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Refund> New(string marketplaceId, string debitId, int? amount = null, string description = null)
        {
            throw new NotImplementedException();
        }

        public Status<Refund> Get(string marketplaceId, string refundId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Refund>> List(string marketplaceId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Refund>> List(string marketplaceId, string accountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Refund> Update(string marketplaceId, string description = null, Dictionary<string, string> meta = null)
        {
            throw new NotImplementedException();
        }
    }
}
