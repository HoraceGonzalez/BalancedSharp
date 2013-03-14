using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// Holds are a type of authorization that reserves (i.e. holds)
    /// a dollar amount on the customer's credit card, usually within 7 days.
    /// A successful hold can be captured, and as a result, creates a debit.
    /// </summary>
    public interface IHoldClient
    {
        /// <summary>
        /// Creates a hold against a card.
        /// Returns a uri that can later be used to
        /// create a debit, up to the full amount of the hold.
        /// </summary>
        /// <param name="holdUri">The hold uri.</param>
        /// <param name="amount">Value must be greater than or equal to the minimum debit and less than or equal to the maximum debit amounts allowed for your marketplace.</param>  
        /// <param name="accountUri">The account uri.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="description">The description.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="sourceUri">The source uri.</param>
        /// <param name="cardUri">The card uri.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Create(string holdUri, int amount, string accountUri = null,
            string appearsOnStatementAs = null, string description = null, Dictionary<string, string> meta = null,
            string sourceUri = null, string cardUri = null);

        /// <summary>
        /// Retrieves the details of a hold
        /// that you've previously created.
        /// </summary>
        /// <param name="holdUri">The hold id.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Get(string holdUri);
       
        /// <summary>
        /// Returns a list of holds you've previously
        /// created against a specific account.
        /// The holds are returned in sorted order, with
        /// the most recent holds appearing first.
        /// </summary>
        /// <param name="holdUri">The hold id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of hold details</returns>
        Status<PagedList<Hold>> List(string holdUri, int limit = 10, int offset = 0);

        /// <summary>
        /// Updates information about a hold.
        /// </summary>
        /// <param name="holdUri">The hold uri.</param>
        /// <param name="description">The description.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="isVoid">Determines whether or not the hold is valid.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Update(string holdUri, string description = null,
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null);

        /// <summary>
        /// Captures a hold. This creates a debit.
        /// </summary>
        /// <param name="holdUri">The hold uri.</param>
        /// <returns>Hold details</returns>
        Status<Debit> Capture(string holdUri, string appearsOnStatementAs, string description);

        /// <summary>
        /// Voids a hold. This cancels the hold.
        /// After voiding, the hold can no
        /// longer be captured.
        /// This operation is irreversible.
        /// </summary>
        /// <param name="holdId">The hold uri.</param>
        /// <returns>Hold details</returns>
        Status<Hold> Void(string holdUri, string appearsOnStatementAs, string description);
    }

    public class HoldClient : IHoldClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public HoldClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Hold> Create(string holdUri, int amount, 
            string accountUri = null, string appearsOnStatementAs = null, 
            string description = null, Dictionary<string, string> meta = null, string sourceUri = null, string cardUri = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount", amount.ToString());
            parameters.Add("account_uri", accountUri);
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("description", description);
            parameters.Add("source_uri", sourceUri);
            parameters.Add("card_uri", cardUri);
            if (meta != null)
                foreach (var key in meta.Keys)
                    parameters.Add(string.Format("meta[{0}]", key), meta[key]);
            return rest.GetResult<Hold>(holdUri, this.Service.Key, "", "post", parameters);
        }

        public Status<Hold> Get(string holdUri)
        {
            return rest.GetResult<Hold>(holdUri, this.Service.Key, "", "get", null);
        }

        public Status<PagedList<Hold>> List(string holdUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Hold>>(holdUri, this.Service.Key, "", "get", parameters);
        }        

        public Status<Hold> Update(string holdUri, string description = null, 
            Dictionary<string, string> meta = null, bool? isVoid = null, string appearsOnStatementAs = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("description", description);
            parameters.Add("is_void", isVoid.HasValue ? isVoid.Value.ToString() : "");
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            if (meta != null)
                foreach (var key in meta.Keys)
                    parameters.Add(string.Format("meta[{0}]", key), meta[key]);

            return rest.GetResult<Hold>(holdUri, this.Service.Key, "", "put", parameters);
        }

        public Status<Hold> Void(string holdUri, string appearsOnStatementAs, string description)
        {
            return Update(holdUri, description, null, true, appearsOnStatementAs);
        }

        public Status<Debit> Capture(string holdUri, string appearsOnStatementAs, string description)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("hold_uri", holdUri);
            if (!string.IsNullOrEmpty(appearsOnStatementAs))
                parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            if (!string.IsNullOrEmpty(description))
                parameters.Add("description", description);

            return rest.GetResult<Debit>(holdUri, this.Service.Key, "", "post", parameters);
        }
    }
}
