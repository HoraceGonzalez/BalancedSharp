using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IRefundClient
    {
        Status<Refund> New(string debitId, int? amount = null, string description = null);

        Status<Refund> Get(string refundId);

        Status<PagedList<Refund>> List(int limit = 10, int offset = 0);

        Status<PagedList<Refund>> List(string accountId, int limit = 10, int offset = 0);

        Status<Refund> Update(string description = null, Dictionary<string, string> meta = null);
    }

    public class RefundClient : IRefundClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public RefundClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Refund> New(string debitId, int? amount = null, string description = null)
        {
            throw new NotImplementedException();
        }

        public Status<Refund> Get(string refundId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Refund>> List(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Refund>> List(string accountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Refund> Update(string description = null, Dictionary<string, string> meta = null)
        {
            throw new NotImplementedException();
        }
    }
}
