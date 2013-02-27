using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IAccountClient
    {
    }

    public class AccountClient : IAccountClient
    {
        IBalanceService balanceService;

        public AccountClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
