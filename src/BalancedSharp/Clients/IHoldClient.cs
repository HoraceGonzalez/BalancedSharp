using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IHoldClient
    {
        /// <summary>
        /// Creates a hold against a card.
        /// Returns a uri that can later be used to
        /// create a debit, up to the full amount of the hold.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="amount">Value must be greater than or equal to the minimum debit and less than or equal to the maximum debit amounts allowed for your marketplace.</param>
        /// <param name="accountUri">The account uri.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="description">Sequence of characters.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="sourceUri">The source uri.</param>
        /// <param name="cardUri">The card uri.</param>
        /// <returns>Hold details</returns>
        Status<Hold> New(string accountId, int amount, string accountUri = null,
            string appearsOnStatementAs = null, string description = null, Dictionary<string, string> meta = null,
            string sourceUri = null, string cardUri = null);

        /// <summary>
        /// Retrieves the details of a hold
        /// that you've previously created.
        /// Use the uri that was previously returned,
        /// and the corresponding hold information will be returned.
        /// </summary>
        /// <param name="holdId">The hold id.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Get(string holdId);

        /// <summary>
        /// Returns a list of holds you've previously created.
        /// The holds are returned in sorted order, with the
        /// most recent holds appearing first.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of Hold details</returns>
        Status<PagedList<Hold>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Returns a list of holds you've previously
        /// created against a specific account.
        /// The holds are returned in sorted order, with
        /// the most recent holds appearing first.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of Hold details</returns>
        Status<PagedList<Hold>> List(string accountId, int limit = 10, int offset = 0);

        /// <summary>
        /// Updates information about a hold.
        /// </summary>
        /// <param name="holdId">The hold id.</param>
        /// <param name="description">Sequence of characters.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="isVoid">Flag value, should be true or false.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Update(string holdId, string description = null,
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null);

        /// <summary>
        /// Captures a hold. This creates a debit.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="holdId">The hold id.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Capture(string accountId, string holdId);

        /// <summary>
        /// Voids a hold. This cancels the hold.
        /// After voiding, the hold can no
        /// longer be captured.
        /// This operation is irreversible.
        /// </summary>
        /// <param name="holdId">The hold id.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Void(string holdId);
    }

    public class HoldClient : IHoldClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public HoldClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Hold> New(string accountId, int amount, 
            string accountUri = null, string appearsOnStatementAs = null, 
            string description = null, Dictionary<string, string> meta = null, string sourceUri = null, string cardUri = null)
        {
            string url = string.Format("{0}{1}/accounts/{2}/holds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("account_uri", accountUri);
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("description", description);
            parameters.Add("source_uri", sourceUri);
            parameters.Add("card_uri", cardUri);

            return rest.GetResult<Hold>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Hold> Get(string holdId)
        {
            string url = string.Format("{0}{1}/holds/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, holdId);

            return rest.GetResult<Hold>(url, this.balanceService.Key, "", "get", null);
        }

        public Status<PagedList<Hold>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/holds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Hold>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<PagedList<Hold>> List(string accountId, int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/accounts/{2}/holds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Hold>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<Hold> Update(string holdId, string description = null, 
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null)
        {
            string url = string.Format("{0}{1}/holds/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, holdId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("description", description);
            parameters.Add("is_void", isVoid.ToString().ToLower());
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);

            return rest.GetResult<Hold>(url, this.balanceService.Key, "", "put", parameters);
        }

        public Status<Hold> Capture(string accountId, string holdId)
        {
            string url = string.Format("{0}{1}/accounts/{2}/debits",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string holdUri = string.Format("{0}/holds/{1}", this.balanceService.MarketplaceUrl, holdId);
            parameters.Add("hold_uri", holdUri);

            return rest.GetResult<Hold>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Hold> Void(string holdId)
        {
            string url = string.Format("{0}{1}/holds/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, holdId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("is_void", "true");

            return rest.GetResult<Hold>(url, this.balanceService.Key, "", "put", parameters);
        }
    }
}
