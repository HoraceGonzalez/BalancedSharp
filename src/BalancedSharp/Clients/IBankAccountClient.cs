using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IBankAccountClient
    {
        Status<BankAccount> New(string name, string accountNumber, string routingNumber, BankAccountType type, Dictionary<string, string> meta = null);

        Status<BankAccount> Get(string bankAccountId);

        Status<PagedList<BankAccount>> List(int limit = 10, int offset = 0);

        Status<bool> Delete(string bankAccountId);
    }

    public class BankAccountClient : IBankAccountClient
    {
        IBalancedService balanceService;

        public BankAccountClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<BankAccount> New(string name, string accountNumber, string routingNumber, BankAccountType type, Dictionary<string, string> meta = null)
        {
            throw new NotImplementedException();
        }


        public Status<BankAccount> Get(string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<BankAccount>> List(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Status<bool> Delete(string bankAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
