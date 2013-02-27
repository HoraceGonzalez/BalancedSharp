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
        IBalanceService balanceService;

        public BankClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
