using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICreditClient
    {
    }

    public class CreditClient : ICreditClient
    {
        IBalancedService balanceService;

        public CreditClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
