using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IDebitClient : IBalancedServiceObject
    {
        
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
