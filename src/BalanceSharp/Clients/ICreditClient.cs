using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface ICreditClient
    {
    }

    public class CreditClient : ICreditClient
    {
        IBalanceService balanceService;

        public CreditClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
