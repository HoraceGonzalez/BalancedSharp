using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// Accounts help facilitate managing multiple credit cards, 
    /// debit cards, and bank accounts along with different 
    /// financial transaction operations, i.e. refunds, debits, credits.
    /// </summary>
    public interface IAccountClient : IBalancedServiceObject
    {
        /// <summary>
        /// Creates a new account under the specified marketplace id.
        /// </summary>
        /// <param name="accountsUri">The accounts uri.</param>
        /// <returns>Account details</returns>
        Status<Account> Create(string accountsUri);

        /// <summary>
        /// Adding a card to an account activates the ability to debit 
        /// an account, more specifically, charging a card. 
        /// You can add multiple cards to an account. Balanced associates a 
        /// buyer role to signify whether or not an account has 
        /// a valid credit card, to acquire funds from.
        /// </summary>
        /// <param name="accountId">The accounts uri.</param>
        /// <param name="cardId">The cards uri.</param>
        /// <returns>Account details</returns>
        Status<Account> AddCard(string accountsUri, string cardUri);

        /// <summary>
        /// Adding a bank account to an account activates the ability to credit an account, 
        /// or in this case, initiate a next-day ACH payment.Balanced does not associate 
        /// a role to signify whether or not an account has a valid bank account to send money to.
        /// </summary>
        /// <param name="accountId">The accounts uri.</param>
        /// <param name="bankAccountId">The bank accounts uri.</param>
        /// <returns>Account details</returns>
        Status<Account> AddBankAccount(string accountsUri, string bankAccountUri);

        /// <summary>
        /// A person, or an individual, is a US based individual or a sole proprietor.
        /// Balanced associates a merchant role to signify whether or not an account has been underwritten.
        /// </summary>
        /// <param name="accountsUri">The accounts uri.</param>
        /// <param name="type">Merchant type. It should be one of: person or business.</param>
        /// <param name="phoneNumber">E.164 formatted phone number. Length must be less or equal to 15</param>
        /// <param name="email">The email. RFC-2822 formatted email address.</param>
        /// <param name="meta">The meta. Single level mapping from string keys to string values.</param>
        /// <param name="taxId">The tax id. Length must be between 4 and 9.</param>
        /// <param name="dob">Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="name">The name. Sequence of characters. Length must be less or equal to 128</param>
        /// <param name="city">The city.</param>
        /// <param name="postalCode">The postal code. Postal code. This is known as a zip code in the USA.</param>
        /// <param name="address">The street address.</param>
        /// <param name="countryCode">The country code. ISO-3166-3 three character country code.</param>
        /// <returns></returns>
        Status<Account> UnderwriteAsIndividual(string accountsUri, string phoneNumber,
            string email = null, Dictionary<string, string> meta = null, string taxId = null,
            string dob = null, string name = null, string city = null, string postalCode = null,
            string address = null, string countryCode = null);

        /// <summary>
        /// Balanced associates a merchant role to signify whether or not an account has been underwritten.
        /// </summary>
        /// <param name="accountsUri">The accounts uri.</param>
        /// <param name="name">Merchant name. Length must be less than or equla to 128.</param>
        /// <param name="phoneNumber">Merchant phone number.</param>
        /// <param name="emailAddress">Merchant email address.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="taxId">Merchant tax id. Length must be between 4 and 9.</param>
        /// <param name="dob">Merchant Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="city">Merchant city.</param>
        /// <param name="postalCode">Merchant postal code. This is known as a zip code in the USA.</param>
        /// <param name="address">Merchant street address.</param>
        /// <param name="countryCode">Merchant country code.</param>
        /// <param name="personName">Individual name.</param>
        /// <param name="personDob">Individual Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="personCity">Individual city.</param>
        /// <param name="personPostalCode">Individual postal code. This is known as a zip code in the USA.</param>
        /// <param name="personAddress">Individual street address.</param>
        /// <param name="personCountryCode">Individual three character country code.</param>
        /// <param name="personTaxId">Individual tax id. Length must be between 4 and 9.</param>
        /// <returns></returns>
        Status<Account> UnderwriteAsBusiness(string accountsUri, string name, string phoneNumber,
            string emailAddress = null, Dictionary<string, string> meta = null, string taxId = null, string dob = null,
            string city = null, string postalCode = null, string address = null,  string countryCode = null,
            string personName = null, string personDob = null, string personCity = null, string personPostalCode = null,
            string personAddress = null, string personCountryCode = null, string personTaxId = null);
        
        /// <summary>
        /// Retrieves the details of an account that has previously been created. 
        /// </summary>
        /// <param name="accountsUri">The account .</param>
        /// <returns>BankAccount details</returns>
        Status<Account> Get(string accountsUri);                
    }

    public class AccountClient : IAccountClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public AccountClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Account> Create(string accountsUri)
        {
            return this.rest.GetResult<Account>(accountsUri, this.Service.Key, null, "post", null);
        }

        public Status<Account> AddCard(string accountsUri, string cardUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("card_uri", cardUri);
            return this.rest.GetResult<Account>(accountsUri, this.Service.Key, null, "put", parameters);
        }

        public Status<Account> AddBankAccount(string accountsUri, string bankAccountUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("bank_account_uri", bankAccountUri);
            return this.rest.GetResult<Account>(accountsUri, this.Service.Key, null, "put", parameters);
        }

        public Status<Account> UnderwriteAsIndividual(string accountsUri, string phoneNumber,
            string email = null, Dictionary<string, string> meta = null, string taxId = null, string dob = null,
            string name = null, string city = null, string postalCode = null, string address = null,
            string countryCode = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("merchant[type]", "person");
            parameters.Add("merchant[phone_number]", phoneNumber);
            parameters.Add("merchant[email]", email);
            parameters.Add("merchant[tax_id]", taxId);
            parameters.Add("merchant[dob]", dob);
            parameters.Add("merchant[name]", name);
            parameters.Add("merchant[city]", city);
            parameters.Add("merchant[postal_code]", postalCode);
            parameters.Add("merchant[street_address]", address);
            parameters.Add("merchant[country_code]", countryCode);
            return rest.GetResult<Account>(accountsUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Account> UnderwriteAsBusiness(string accountsUri, string name, string phoneNumber,
            string emailAddress = null, Dictionary<string, string> meta = null, string taxId = null, string dob = null,
            string city = null, string postalCode = null, string address = null, string countryCode = null,
            string personName = null, string personDob = null, string personCity = null, string personPostalCode = null,
            string personAddress = null, string personCountryCode = null, string personTaxId = null)
        {          
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("merchant[type]", "business");
            parameters.Add("merchant[name]", name);
            parameters.Add("merchant[phone_number]", phoneNumber);
            parameters.Add("merchant[email_address]", emailAddress);
            parameters.Add("merchant[tax_id]", taxId);
            parameters.Add("merchant[dob]", dob);
            parameters.Add("merchant[city]", city);
            parameters.Add("merchant[postal_code]", postalCode);
            parameters.Add("merchant[country_code]", countryCode);
            parameters.Add("merchant[street_address]", address);
            parameters.Add("merchant[person[name]]", personName);
            parameters.Add("merchant[person[dob]]", personDob);
            parameters.Add("merchant[person[city]]", personCity);
            parameters.Add("merchant[person[postal_code]]", personPostalCode);
            parameters.Add("merchant[person[street_address]]", personAddress);
            parameters.Add("merchant[person[country_code]]", personCountryCode);
            parameters.Add("merchant[person[tax_id]]", personTaxId);
            return rest.GetResult<Account>(accountsUri, this.Service.Key, null, "post", parameters);
        }

        public Status<Account> Get(string accountsUri)
        {
            return this.rest.GetResult<Account>(accountsUri, this.Service.Key, null, "get", null);
        }
    }
}
