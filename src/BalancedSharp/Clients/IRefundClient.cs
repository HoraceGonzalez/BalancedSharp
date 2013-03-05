using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{

    public interface IRefundClient
    {
        /// <summary>
        /// Issues a refund from a debit. You can either refund the full amount of the 
        /// debit or you can issue a partial refund, where the amount is less than the 
        /// charged amount.
        /// </summary>
        /// <param name="debitId">Id of the debited amount.</param>
        /// <param name="accountId">The account Id.</param>
        /// <param name="amount">Amount of the resolving URI. Can be the total amount or a partial refund.</param>
        /// <param name="description">Sequence of characters.</param>       
        /// <returns> New refund</returns>
        Status<Refund> New(string debitId, string accountId, int? amount = null, string description = null);

        /// <summary>
        /// Retrieves the details of a refund that you've previously created. Use the 
        /// uri that was previously returned, and the corresponding refund information
        /// will be returned.
        /// </summary>
        /// <param name="refundId">Id of the refund.</param> 
        /// <returns>Refund details</returns>
        Status<Refund> Get(string refundId);

        /// <summary>
        /// Returns a list of refunds you've previously created. The refunds are returned in sorted order,
        /// with the most recent refunds appearing first.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of refunds</returns>
        Status<PagedList<Refund>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Returns a list of refunds you've previously created against a specific account. The refunds are returned in sorted order,
        /// with the most recent refunds appearing first.
        /// </summary>
        /// <param name="accountId">The account Id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of refunds by account</returns>
        Status<PagedList<Refund>> List(string accountId, int limit = 10, int offset = 0);

        /// <summary>
        ///Updates information about a refund
        /// </summary>
        /// <param name="refundId">Id of the refund.</param> 
        /// <param name="description">Sequence of characters.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <returns>Udated refund details</returns>
        Status<Refund> Update(string refundId, string description = null, Dictionary<string, string> meta = null);
    }

    public class RefundClient : IRefundClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public RefundClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Refund> New(string debitId, string accountId, int? amount = null, string description = null)
        {
            string url = string.Format("{0}{1}/accounts/{2}/refunds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            string debitUri = string.Format("{0}/debits/{1}", this.balanceService.MarketplaceUrl, debitId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("debit_uri", debitUri);
            if (amount.HasValue) parameters.Add("amount", amount.Value.ToString());
            parameters.Add("description", description);
            
            return rest.GetResult<Refund>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Refund> Get(string refundId)
        {
            string url = string.Format("{0}{1}/refunds/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, refundId);
           
            return rest.GetResult<Refund>(url, this.balanceService.Key, "", "get", null);
        }

        public Status<PagedList<Refund>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/refunds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, limit);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Refund>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<PagedList<Refund>> List(string accountId, int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/accounts/{2}/refunds",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Refund>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<Refund> Update(string refundId, string description = null, Dictionary<string, string> meta = null)
        {
            string url = string.Format("{0}{1}/refunds/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, refundId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("description", description);
          
            return rest.GetResult<Refund>(url, this.balanceService.Key, "", "put", parameters);
        }
    }
}
