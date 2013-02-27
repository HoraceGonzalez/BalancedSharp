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
        IBalanceService balanceService;

        public CardClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
