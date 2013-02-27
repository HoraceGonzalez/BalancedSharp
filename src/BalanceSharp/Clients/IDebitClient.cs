using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IDebitClient
    {
    }

    public class DebitClient : IDebitClient
    {
        IBalanceService balanceService;

        public DebitClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
