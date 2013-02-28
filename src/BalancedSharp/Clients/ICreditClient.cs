using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICreditClient
    {
        Status<Credit> New(int amount, string name, string accountNumber, 
            string routingNumber, BankAccountType type, Dictionary<string, string> meta);

        Status<Credit> New(string bankAccountId, int amount, string description = null);

        Status<Credit> New(string accountId, int amount, string description = null,
            Dictionary<string, string> meta = null, string appearsOnStatementAs = null, string destinationUrl = null,
            string bankAccountUri = null);

        Status<Credit> Get(string creditId);

        Status<PagedList<Credit>> List(int limit = 10, int offset = 0);

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

        public Status<Credit> New(int amount, string name, string accountNumber, 
            string routingNumber, BankAccountType type, Dictionary<string, string> meta)
        {
            throw new NotImplementedException();
        }

        public Status<Credit> New(string bankAccountId, int amount, string description = null)
        {
            throw new NotImplementedException();
        }

        public Status<Credit> New(string accountId, int amount, 
            string description = null, Dictionary<string, string> meta = null, string appearsOnStatementAs = null, 
            string destinationUrl = null, string bankAccountUri = null)
        {
            throw new NotImplementedException();
        }

        public Status<Credit> Get(string creditId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Credit>> List(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Credit>> List(string bankAccountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }
}
