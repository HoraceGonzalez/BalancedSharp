using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICreditClient
    {
        /// <summary>
        /// To credit a new bank account, you simply pass
        /// the amount along with the bank account details.
        /// We do not store this bank account when you create a credit this
        /// way, so you can safely assume that the information has been deleted.
        /// </summary>
        /// <param name="amount">USD cents. You must have amount funds transferred to cover the credit.</param>
        /// <param name="name">Name on the bank account. Length must be greater than or equal to 2.</param>
        /// <param name="accountNumber">Bank account number. Length must be greater than or equal to 1.</param>
        /// <param name="bankCode">Bank account code. Length must be equal to 9.</param>
        /// <param name="routingNumber">Bank account code. Length must be equal to 9.</param>
        /// <param name="type">Bank type (checking or savings).</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <returns>Credit details</returns>
        Status<Credit> New(int amount, string name, string accountNumber, string bankCode,
            string routingNumber, BankAccountType type, Dictionary<string, string> meta = null);

        /// <summary>
        /// To credit an existing bank account, you simply pass the
        /// amount to the nested credit endpoint of a bank account.
        /// The credits_uri is a convenient uri provided so that you
        /// can simply issue a POST with the amount and a credit shall be created.
        /// </summary>
        /// <param name="bankAccountId">The bank account id.</param>
        /// <param name="amount">USD cents. You must have amount funds transferred to cover the credit.</param>
        /// <param name="description">Sequence of characters.</param>
        /// <returns>Credit details</returns>
        Status<Credit> New(int amount, string bankAccountId, string description = null);

        /// <summary>
        /// Creates a new credit for an account.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="amount">USD cents. Must be greater than or equal your minimum credit amount but less than or equal to your maximum credit amount.</param>
        /// <param name="description">Sequence of characters.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="destinationUrl">The destination url.</param>
        /// <param name="bankAccountUri">The bank account uri.</param>
        /// <returns></returns>
        Status<Credit> New(string accountId, int amount, string description = null,
            Dictionary<string, string> meta = null, string appearsOnStatementAs = null, string destinationUrl = null,
            string bankAccountUri = null);

        /// <summary>
        /// Retrieves the details of a credit that you've previously created.
        /// Use the uri that was previously returned,
        /// and the corresponding credit information will be returned.
        /// </summary>
        /// <param name="creditId">The credit id.</param>
        /// <returns>Credit details</returns>
        Status<Credit> Get(string creditId);

        /// <summary>
        /// Returns a list of credits you've previously created.
        /// The credits are returned in sorted order, with
        /// the most recent credits appearing first.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of Credit details</returns>
        Status<PagedList<Credit>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Returns a list of credits you've
        /// previously created to a specific bank account.
        /// The credits are returned in sorted order, with
        /// the most recent credits appearing first.
        /// </summary>
        /// <param name="bankAccountId">The bank account id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of Credit details</returns>
        Status<PagedList<Credit>> List(string bankAccountId, int limit = 10, int offset = 0);
    }

    public class CreditClient : ICreditClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public CreditClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Credit> New(int amount, string name, string accountNumber, string bankCode,
            string routingNumber, BankAccountType type, Dictionary<string, string> meta =  null)
        {
            string url = string.Format("{0}/credits", this.balanceService.BaseUri);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("name", name);
            parameters.Add("account_number", accountNumber);
            parameters.Add("bank_code", bankCode);

            return rest.GetResult<Credit>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Credit> New(int amount, string bankAccountId, string description = null)
        {
            string url = string.Format("{0}/bank_accounts/{1}/credits",
                this.balanceService.BaseUri, bankAccountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("description", description);

            return rest.GetResult<Credit>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Credit> New(string accountId, int amount, 
            string description = null, Dictionary<string, string> meta = null, string appearsOnStatementAs = null, 
            string destinationUrl = null, string bankAccountUri = null)
        {
            string url = string.Format("{0}{1}/accounts/{2}/credits",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("description", description);
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("destination_uri", destinationUrl);
            parameters.Add("bank_account_uri", bankAccountUri);

            return rest.GetResult<Credit>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Credit> Get(string creditId)
        {
            string url = string.Format("{0}/credits/{1}",
                this.balanceService.BaseUri, creditId);

            return rest.GetResult<Credit>(url, this.balanceService.Key, "", "get", null);
        }

        public Status<PagedList<Credit>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}/credits", this.balanceService.BaseUri);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Credit>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<PagedList<Credit>> List(string bankAccountId, int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}/bank_accounts/{1}/credits",
                this.balanceService.BaseUri, bankAccountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Credit>>(url, this.balanceService.Key, "", "get", parameters);
        }
    }
}
