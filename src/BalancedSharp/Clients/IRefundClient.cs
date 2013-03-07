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
        /// <param name="refundUri">The refund Id.</param>
        /// <param name="amount">Amount of the resolving URI. Can be the total amount or a partial refund.</param>
        /// <param name="description">Sequence of characters.</param>       
        /// <returns> New refund</returns>
        Status<Refund> Create(string refundUri, int? amount = null, string description = null);

        /// <summary>
        /// Retrieves the details of a refund that you've previously created. Use the 
        /// uri that was previously returned, and the corresponding refund information
        /// will be returned.
        /// </summary>
        /// <param name="refundUri">The refund Id.</param>
        /// <returns>Refund details</returns>
        Status<Refund> Get(string refundUri);

        /// <summary>
        /// Returns a list of refunds you've previously created. The refunds are returned in sorted order,
        /// with the most recent refunds appearing first.
        /// </summary>
        /// <param name="refundUri">The refund Id.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>List of refunds</returns>
        Status<PagedList<Refund>> List(string refundUri, int limit = 10, int offset = 0);
        
        /// <summary>
        ///Updates information about a refund
        /// </summary>
        /// <param name="refundUri">The refund Id.</param>
        /// <param name="description">Sequence of characters.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <returns>Udated refund details</returns>
        Status<Refund> Update(string refundUri, string description = null, Dictionary<string, string> meta = null);
    }

    public class RefundClient : IRefundClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public RefundClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Refund> Create(string refundUri, int? amount = null, string description = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();            
            if (amount.HasValue) parameters.Add("amount", amount.Value.ToString());
            parameters.Add("description", description);
            
            return rest.GetResult<Refund>(refundUri, this.Service.Key, "", "post", parameters);
        }

        public Status<Refund> Get(string refundUri)
        {
            return rest.GetResult<Refund>(refundUri, this.Service.Key, "", "get", null);
        }

        public Status<PagedList<Refund>> List(string refundUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Refund>>(refundUri, this.Service.Key, "", "get", parameters);
        }        

        public Status<Refund> Update(string refundUri, string description = null, Dictionary<string, string> meta = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("description", description);
          
            return rest.GetResult<Refund>(refundUri, this.Service.Key, "", "put", parameters);
        }
    }
}
