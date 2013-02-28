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
        /// <returns>BankAccount details</returns>
        Status<BankAccount> Create(string name, string accountNumber, string routingNumber, 
            BankAccountType type, Dictionary<string, string> meta = null);

        /// <summary>
        /// Retrieves the details of a bank account that has previously been created. 
        /// The same information is returned when creating the bank account.
        /// </summary>
        /// <param name="bankAccountId">The bank account id.</param>
        /// <returns>BankAccount details</returns>
        Status<BankAccount> Get(string bankAccountId);

        /// <summary>
        /// Returns a list of bank accounts that you've created but haven't deleted.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of BankAccount details</returns>
        Status<PagedList<BankAccount>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Permanently delete a bank account. It cannot be undone. All associated credits 
        /// with a deleted bank account will not be affected.
        /// </summary>
        /// <param name="bankAccountId">The bank account id.</param>
        /// <returns></returns>
        Status Delete(string bankAccountId);
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
            parameters.Add("name", name);
            parameters.Add("account_number", accountNumber);
            parameters.Add("routing_number", routingNumber);
            parameters.Add("type", type.ToString().ToLower());
            return this.rest.GetResult<BankAccount>(url, this.balanceService.Key, null, "post", parameters);
        }

        public Status<BankAccount> Get(string bankAccountId)
        {
            string url = string.Format("{0}/v1/bank_accounts/{1}", 
                this.balanceService.BaseUri, bankAccountId);
            return this.rest.GetResult<BankAccount>(url, this.balanceService.Key, null, "get", null);
        }

        public Status<PagedList<BankAccount>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}/v1/bank_accounts", this.balanceService.BaseUri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<BankAccount>>(url, this.balanceService.Key, null, "get", parameters);
        }

        public Status Delete(string bankAccountId)
        {
            string url = string.Format("{0}/v1/bank_accounts/{1}", 
                this.balanceService.BaseUri, bankAccountId);
            return this.rest.GetResult(url, this.balanceService.Key, null, "delete", null);
        }
    }
}
