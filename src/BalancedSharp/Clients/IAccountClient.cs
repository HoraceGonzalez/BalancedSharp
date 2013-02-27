using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IAccountClient
    {
    }

    public class AccountClient : IAccountClient
    {
        IBalancedService balanceService;

        public AccountClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
