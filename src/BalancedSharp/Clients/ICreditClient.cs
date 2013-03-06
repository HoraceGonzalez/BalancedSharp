using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// To credit a bank account, you must create a new credit resource.
    /// </summary>
    public interface ICreditClient : IBalancedServiceObject
    {
        // TODO:
        // Creating A New Credit For An Account

        /// <summary>
        /// Credits a new bank account.
        /// We do not store this bank account when you create a credit this
        /// way, so you can safely assume that the information has been deleted.
        /// </summary>
        /// <param name="amount">The amount in USD cents. You must have amount funds transferred to cover the credit.</param>
        /// <param name="bankAccount">The bank account.</param>
        /// <param name="description">The description.</param>
        /// <returns>Credit details</returns>
        Credit New(int amount, BankAccount bankAccount, string description = null);

        Status<Credit> Save(Credit credit);

        /// <summary>
        /// Retrieves the details of a credit that you've previously created.
        /// Use the uri that was previously returned, and the corresponding
        /// credit information will be returned.
        /// </summary>
        /// <param name="creditId">The credit uri.</param>
        /// <returns>Credit details</returns>
        Status<Credit> Get(string creditUri);

        /// <summary>
        /// Returns a list of credits you've previously created.
        /// The credits are returned in sorted order, with
        /// the most recent credits appearing first.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of credit details</returns>
        Status<PagedList<Credit>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Returns a list of credits you've previously
        /// created to a specific bank account/account.
        /// The credits are returned in sorted order, with
        /// the most recent credits appearing first.
        /// </summary>
        /// <param name="uri">The uri.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of credit details</returns>
        Status<PagedList<Credit>> List(string uri, int limit = 10, int offset = 0);
    }

    public class CreditClient : ICreditClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public CreditClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Credit New(int amount, BankAccount bankAccount, string description = null)
        {
            return new Credit
            {
                Amount = amount,
                BankAccount = bankAccount,
                Service = this.Service
            };
        }

        public Status<Credit> Save(Credit credit)
        {
            string url = string.Format("{0}/v1/credits", this.Service.BaseUri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", credit.Amount.ToString());
            parameters.Add("bank_account[name]", credit.BankAccount.Name);
            parameters.Add("bank_account[account_number]", credit.BankAccount.AccountNumber);
            parameters.Add("bank_account[routing_number]", credit.BankAccount.RoutingNumber);
            parameters.Add("bank_account[type]", credit.BankAccount.Type.ToString().ToLower());
            parameters.Add("description", credit.Description);
            return this.rest.GetResult<Credit>(url, this.Service.Key, null, "post", parameters);
        }

        public Status<Credit> Get(string creditUri)
        {
            string url = string.Format("{0}{1}", this.Service.BaseUri, creditUri);
            return this.rest.GetResult<Credit>(url, this.Service.Key, null, "get", null);
        }

        public Status<PagedList<Credit>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}/v1/credits", this.Service.BaseUri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Credit>>(url, this.Service.Key, null, "get", parameters);
        }

        public Status<PagedList<Credit>> List(string uri, int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/credits", this.Service.BaseUri, uri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Credit>>(url, this.Service.Key, null, "get", parameters);
        }
    }
}
