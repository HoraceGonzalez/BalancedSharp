using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IDebitClient
    {
    }

    public class DebitClient : IDebitClient
    {
        IBalancedService balanceService;

        public DebitClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
