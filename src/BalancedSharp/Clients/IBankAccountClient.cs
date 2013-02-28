using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IBankAccountClient
    {
        /// <summary>
        /// You'll eventually want to be able to credit bank accounts 
        /// without having to ask your users for their information 
        /// over and over again. To do this, you'll need to create a bank account object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="routingNumber">The routing number. Length must be = 9.</param>
        /// <param name="type">The type.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <returns></returns>
        Status<BankAccount> Create(string name, string accountNumber, string routingNumber, 
            BankAccountType type, Dictionary<string, string> meta = null);

        Status<BankAccount> Get(string bankAccountId);

        Status<PagedList<BankAccount>> List(int limit = 10, int offset = 0);

        Status<bool> Delete(string bankAccountId);
    }

    public class BankAccountClient : IBankAccountClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public BankAccountClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<BankAccount> Create(string name, string accountNumber, string routingNumber, 
            BankAccountType type, Dictionary<string, string> meta = null)
        {
            string url = string.Format("{0}/v1/bank_accounts", this.balanceService.BaseUri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            return this.rest.GetResult<BankAccount>(url, this.balanceService.Key, null, "post", parameters);
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
