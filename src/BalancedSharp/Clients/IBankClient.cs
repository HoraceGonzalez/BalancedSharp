using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IBankClient
    {
    }

    public class BankClient : IBankClient
    {
        IBalancedService balanceService;

        public BankClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
