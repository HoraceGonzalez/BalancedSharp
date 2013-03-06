using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IDebitClient : IBalancedServiceObject
    {
        /// <summary>
        /// Debits an account.
        /// Successful creation of a debit using a card will
        /// return an associated hold mapping as part of the response.
        /// This hold was created and captured behind the scenes automatically. 
        /// For ACH debits there is no corresponding hold.
        /// </summary>
        /// <param name="accountUri">The account uri.</param>
        /// <param name="amount">If the resolving URI references a hold then this is hold amount. You can always capture less than the hold amount (e.g. a partial capture). Otherwise its the maximum per debit amount for your marketplace. Value must be greater than or equal to the minimum per debit amount for your marketplace. Value must be less than or equal to the maximum per debit amount for marketplace.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="description">The description.</param>        
        /// <param name="onBehalfOfUri">The account of a merchant, usually a seller or service provider, that is associated with this card charge or bank account debit.</param>
        /// <param name="holdUri">If no hold is provided one my be generated and captured if the funding source is a card.</param>
        /// <param name="sourceUri">URI of a specific bank account or card to be debited.</param>
        /// <returns>Debit details</returns>
        Status<Debit> New(string bankAccountUri, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null);

        /// <summary>
        /// Retrieves the details of a created debit.
        /// </summary>
        /// <param name="debitUri">The debit uri.</param>
        /// <returns>Debit details</returns>
        Status<Debit> Get(string debitUri);

        /// <summary>
        /// Returns a list of debits you've previously created.
        /// The debits are returned in sorted order, with the
        /// most recent debits appearing first.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of debit details</returns>
        Status<PagedList<Debit>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Returns a list of debits you've previously
        /// created against a specific account.
        /// The debits are returned in sorted order, with
        /// the most recent debits appearing first.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of debit details</returns>
        Status<PagedList<Debit>> List(string accountId, int limit = 10, int offset = 0);

        /// <summary>
        /// Updates information about a debit.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="debitId">The debit id.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        Status<Debit> Update(string accountId, string debitId,
            Dictionary<string, string> meta = null, string description = null);
    }

    public class DebitClient : IDebitClient
    {
        IBalancedRest rest;

        public DebitClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Debit> New(string accountUri, int? amount = null, 
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null, 
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null)
        {
            string url = string.Format("{0}/v1/{1}accounts/{2}/debits", this.Service.BaseUri,
                this.Service.MarketplaceUrl, accountUri);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (amount.HasValue) parameters.Add("amount", amount.Value.ToString());
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("description", description);
            parameters.Add("account_uri", accountUri);
            parameters.Add("on_behalf_of_uri", onBehalfOfUri);
            parameters.Add("hold_uri", holdUri);
            parameters.Add("source_uri", sourceUri);

            return rest.GetResult<Debit>(url, this.Service.Key, "", "post", parameters);
        }

        public Status<Debit> Get(string debitUri)
        {
            string url = string.Format("{0}/{1}/debits/{2}",
                this.Service.BaseUri, this.Service.MarketplaceUrl, debitUri);

            return rest.GetResult<Debit>(url, this.Service.Key, "", "get", null);
        }

        public Status<PagedList<Debit>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}/{1}/debits",
                this.Service.BaseUri, this.Service.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Debit>>(url, this.Service.Key, "", "get", parameters);
        }

        //public Status<PagedList<Debit>> List(string accountId, 
        //    int limit = 10, int offset = 0)
        //{
        //    string url = string.Format("{0}{1}/accounts/{2}/debits",
        //        this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("limit", limit.ToString());
        //    parameters.Add("offset", offset.ToString());

        //    return rest.GetResult<PagedList<Debit>>(url, this.balanceService.Key, "", "get", parameters);
        //}

        //public Status<Debit> Update(string accountId, 
        //    string debitId, Dictionary<string, string> meta = null, string description = null)
        //{
        //    string url = string.Format("{0}{1}/accounts/{2}/debits/{3}",
        //        this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId,debitId);

        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("description", description);

        //    return rest.GetResult<Debit>(url, this.balanceService.Key, "", "put", parameters);
        //}

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
