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
        Status<Debit> Create(string accountUri, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null);
        
        /// <summary>
        /// Retrieves the details of a debit.
        /// </summary>
        /// <param name="debitUri">The debit uri.</param>
        /// <returns>Debit details</returns>
        Status<Debit> Get(string debitUri);

        /// <summary>
        /// Returns a list of debits you've previously
        /// created against a specific account. Or 
        /// a list of debits you've previously created
        /// on the marketplace. based on the uri parameter
        /// The debits are returned in sorted order, with
        /// the most recent debits appearing first.
        /// </summary>
        /// <param name="debitsUri">The debit uri.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of debit details for a specific account</returns>
        Status<PagedList<Debit>> List(string debitsUri, int limit = 10, int offset = 0);

        /// <summary>
        /// Updates information about a debit.
        /// </summary>
        /// <param name="debitUri">The debitUri.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        Status<Debit> Update(string debitUri, Dictionary<string, string> meta = null,
            string description = null);

        /// <summary>
        /// Permanently deletes a debit.
        /// This cannot be undone.
        /// </summary>
        /// <param name="debitUri">The debit uri.</param>
        /// <returns></returns>
        Status Delete(string bankAccountUri);
    }

    public class DebitClient : IDebitClient
    {
        IBalancedRest rest;

        public DebitClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Debit> Create(string accountUri, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (amount.HasValue) parameters.Add("amount", amount.Value.ToString());
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("description", description);
            parameters.Add("account_uri", accountUri);
            parameters.Add("on_behalf_of_uri", onBehalfOfUri);
            parameters.Add("hold_uri", holdUri);
            parameters.Add("source_uri", sourceUri);

            return rest.GetResult<Debit>(accountUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Debit> Get(string debitUri)
        {
            return rest.GetResult<Debit>(debitUri, this.Service.Key, null, "get", null);
        }        

        public Status<PagedList<Debit>> List(string debitUri,
            int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Debit>>(debitUri, this.Service.Key, null, "get", parameters);
        }

        public Status<Debit> Update(string debitUri, Dictionary<string, string> meta = null,
            string description= null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("description", description);
            foreach (var key in meta.Keys)
                parameters.Add(string.Format("meta[{0}]", key), meta[key]);

            return rest.GetResult<Debit>(debitUri, this.Service.Key, null, "put", parameters);
        }

        public Status Delete(string debitUri)
        {
            return this.rest.GetResult(debitUri, this.Service.Key, null, "delete", null);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
