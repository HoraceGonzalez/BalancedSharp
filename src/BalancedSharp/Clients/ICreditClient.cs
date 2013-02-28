using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICreditClient
    {
        Status<Credit> New(int amount, string name, string accountNumber, 
            string routingNumber, BankAccountType type, string meta);

        Status<Credit> New(string bankAccountId, int amount, string description = null);

        Status<Credit> New(string marketplaceId, string accountId, int amount, string description = null,
            string meta = null, string appearsOnStatementAs = null, string destinationUrl = null,
            string bankAccountUri = null);

        Status<Credit> Get(string creditId);

        Status<PagedList<Credit>> List(int limit = 10, int offset = 0);

        Status<PagedList<Credit>> List(string bankAccountId, int limit = 10, int offset = 0);

        Status<PagedList<Credit>> List(string marketplaceId, string bankAccountId, int limit = 10, int offset = 0);
    }

    public class CreditClient : ICreditClient
    {
        IBalancedService balanceService;

        public CreditClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }
    }
}
