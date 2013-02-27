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
        IBalancedService balanceService;

        public RefundClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
