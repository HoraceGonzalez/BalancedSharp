using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IDebitClient : IBalancedServiceObject
    {
        
        /// <summary>
        /// Retrieves the details of a debit.
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

        ///// <summary>
        ///// Updates information about a debit.
        ///// </summary>
        ///// <param name="debitUri">The debitUri.</param>
        ///// <param name="meta">Single level mapping from string keys to string values.</param>
        ///// <param name="description">The description.</param>
        ///// <returns></returns>
        //Debit Update(string accountId, string debitId,
        //    Dictionary<string, string> meta = null, string description = null);

        //Status<Debit> Save(Debit debit);
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

        //public Debit Update()
        //{
        //    return new Debit()
        //    {
        //    }
        //}

        //public Status<Debit> Save(Debit debit)
        //{
        //    return null;
        //}

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
