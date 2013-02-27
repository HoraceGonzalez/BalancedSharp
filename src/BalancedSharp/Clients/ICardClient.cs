using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface ICardClient
    {
    }

    public class CardClient : ICardClient
    {
        IBalancedService balanceService;

        public CardClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
