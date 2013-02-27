using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IBankClient
    {
        Status<Bank> New(string name, string accountNumber, string routingNumber, BankType type, string meta = null);
    }

    public class BankClient : IBankClient
    {
        IBalancedService balanceService;

        public BankClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Bank> New(string name, string accountNumber, string routingNumber, BankType type, string meta = null)
        {
            throw new NotImplementedException();
        }
    }
}
