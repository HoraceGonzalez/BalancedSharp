using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IHoldClient
    {
    }

    public class HoldClient : IHoldClient
    {
        IBalanceService balanceService;

        public HoldClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
