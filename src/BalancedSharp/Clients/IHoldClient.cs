using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IHoldClient
    {
    }

    public class HoldClient : IHoldClient
    {
        IBalancedService balanceService;

        public HoldClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
