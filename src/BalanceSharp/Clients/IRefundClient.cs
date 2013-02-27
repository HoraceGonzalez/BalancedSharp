using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IRefundClient
    {
    }

    public class RefundClient : IRefundClient
    {
        IBalanceService balanceService;

        public RefundClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
