using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalanceSharp.Clients
{
    public interface IEventClient
    {
    }

    public class EventClient : IEventClient
    {
        IBalanceService balanceService;

        public EventClient(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
