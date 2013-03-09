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
        /// <param name="creditsUri">The credits uri.</param>
        /// <param name="amount">The amount in USD cents. You must have amount funds transferred to cover the credit.</param>
        /// <param name="name">Name on the bank account.</param>
        /// <param name="accountNumber">Bank account number.</param>
        /// <param name="routingNumber">Bank account code.</param>
        /// <param name="type">Bank account type. checking or savings.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="description">The description.</param>
        /// <returns>Credit details</returns>
        Status<Credit> CreateNewBank(string creditsUri, int amount, string name, string accountNumber,
            string routingNumber, string type, Dictionary<string, string> meta = null, string description = null);

        /// <summary>
        /// To credit an existing bank account, you simply pass the amount to
        /// the nested credit endpoint of a bank account.
        /// The credits_uri is a convenient uri provided so that you can simply
        /// issue a POST with the amount and a credit shall be created.
        /// </summary>
        /// <param name="bankAccountUri">The credits uri.</param>
        /// <param name="amount">The amount in USD cents. You must have amount funds transferred to cover the credit.</param>
        /// <param name="description">The description.</param>
        /// <returns>Credit details</returns>
        Status<Credit> CreateBank(string bankAccountUri, int amount, string description = null);

        /// <summary>
        /// Creates a new credit for an account.
        /// </summary>
        /// <param name="creditsUri">The credits uri.</param>
        /// <param name="amount">Amount in USD cents. </param>
        /// <param name="description">The description.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="destinationUri">The destination uri.</param>
        /// <param name="bankAccountUri">The bank account uri.</param>
        /// <returns>Credit details</returns>
        Status<Credit> CreateAccount(string creditsUri, int amount, string description = null,
            Dictionary<string, string> meta = null, string appearsOnStatementAs = null,
            string destinationUri = null, string bankAccountUri = null);

        /// <summary>
        /// Retrieves the details of a credit that you've previously created.
        /// Use the uri that was previously returned, and the corresponding
        /// credit information will be returned.
        /// </summary>
        /// <param name="creditsUri">The credits uri.</param>
        /// <returns>Credit details</returns>
        Status<Credit> Get(string creditsUri);

        /// <summary>
        /// Returns a list of credits you've previously created.
        /// The credits are returned in sorted order, with
        /// the most recent credits appearing first.
        /// </summary>
        /// <param name="creditsUri">The credits uri.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of credit details</returns>
        Status<PagedList<Credit>> List(string creditsUri, int limit = 10, int offset = 0);
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

        public Status<Credit> CreateNewBank(string creditsUri, int amount, string name, string accountNumber,
            string routingNumber, string type, Dictionary<string, string> meta = null, string description = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("bank_account[name]", name);
            parameters.Add("bank_account[account_number]", accountNumber);
            parameters.Add("bank_account[routing_number]", routingNumber);
            parameters.Add("bank_account[type]", type);
            parameters.Add("description", description);
            return this.rest.GetResult<Credit>(creditsUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Credit> CreateBank(string creditsUri, int amount, string description = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("description", description);

            return this.rest.GetResult<Credit>(creditsUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Credit> CreateAccount(string creditsUri, int amount, string description = null,
            Dictionary<string, string> meta = null, string appearsOnStatementAs = null,
            string destinationUri = null, string bankAccountUri = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("description", description);
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("destination_uri", destinationUri);
            parameters.Add("bank_account_uri", bankAccountUri);
            return this.rest.GetResult<Credit>(creditsUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Credit> Get(string creditsUri)
        {
            return this.rest.GetResult<Credit>(creditsUri, this.Service.Key, null, "get", null);
        }

        public Status<PagedList<Credit>> List(string creditsUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Credit>>(creditsUri, this.Service.Key, null, "get", parameters);
        }
    }
}
