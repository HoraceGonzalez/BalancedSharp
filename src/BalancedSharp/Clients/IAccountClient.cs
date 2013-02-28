using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IAccountClient
    {
        Status<Account> Create(string marketplaceId);

        Status<Account> AddCard(string marketplaceId, string accountId, string cardId);

        Status<Account> AddBankAccount(string marketplaceId, string accountId, string bankAccountId);

        Status<Account> UnderwriteAsIndividual(string marketplaceId, string phoneNumber, string name = null, 
            string dob = null, string streetAddress = null, string city = null, 
            string postalCode = null, string countryCode = null, string email = null,
            string meta = null, string taxId = null);

        Status<Account> UnderwriteAsBusiness(string marketplaceId, string name, string phoneNumber, string emailAddress = null,
            string meta = null, string taxId = null, string dob = null, string city = null, string postalCode = null,
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, string personStreetAddress = null,
            string personCountryCode = null, string personTaxId = null);
    }

    public class AccountClient : IAccountClient
    {
        IBalancedService balanceService;

        public AccountClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Account> Create(string marketplaceId)
        {
            throw new NotImplementedException();
        }

        public Status<Account> AddCard(string marketplaceId, string accountId, string cardId)
        {
            throw new NotImplementedException();
        }

        public Status<Account> AddBankAccount(string marketplaceId, string accountId, string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Status<Account> UnderwriteAsIndividual(string marketplaceId, string phoneNumber, 
            string name = null, string dob = null, string streetAddress = null, 
            string city = null, string postalCode = null, string countryCode = null, 
            string email = null, string meta = null, string taxId = null)
        {
            throw new NotImplementedException();
        }

        public Status<Account> UnderwriteAsBusiness(string marketplaceId, string name, 
            string phoneNumber, string emailAddress = null, string meta = null, 
            string taxId = null, string dob = null, string city = null, string postalCode = null, 
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, 
            string personStreetAddress = null, string personCountryCode = null, string personTaxId = null)
        {
            throw new NotImplementedException();
        }
    }
}
