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
    public interface IAccountClient
    {
        /// <summary>
        /// Creates a new account under the specified marketplace id.
        /// </summary>
        /// <returns>Account details</returns>
        Status<Account> Create();

        /// <summary>
        /// Adding a card to an account activates the ability to debit 
        /// an account, more specifically, charging a card. 
        /// You can add multiple cards to an account. Balanced associates a 
        /// buyer role to signify whether or not an account has 
        /// a valid credit card, to acquire funds from.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="cardId">The card id.</param>
        /// <returns>Account details</returns>
        Status<Account> AddCard(string accountId, string cardId);

        /// <summary>
        /// Adding a bank account to an account activates the ability to credit an account, 
        /// or in this case, initiate a next-day ACH payment.Balanced does not associate 
        /// a role to signify whether or not an account has a valid bank account to send money to.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="bankAccountId">The bank account id.</param>
        /// <returns>Account details</returns>
        Status<Account> AddBankAccount(string accountId, string bankAccountId);

        /// <summary>
        /// A person, or an individual, is a US based individual or a sole proprietor.
        /// Balanced associates a merchant role to signify whether or not an account has been underwritten.
        /// </summary>
        /// <param name="phoneNumber">E.164 formatted phone number. Length must be less or equal to 15</param>
        /// <param name="name">The name. Sequence of characters. Length must be less or equal to 128</param>
        /// <param name="dob">Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="streetAddress">The street address.</param>
        /// <param name="city">The city.</param>
        /// <param name="postalCode">The postal code. Postal code. This is known as a zip code in the USA.</param>
        /// <param name="countryCode">The country code. ISO-3166-3 three character country code.</param>
        /// <param name="email">The email. RFC-2822 formatted email address.</param>
        /// <param name="meta">The meta. Single level mapping from string keys to string values.</param>
        /// <param name="taxId">The tax id. Length must be between 4 and 9.</param>
        /// <returns></returns>
        Status<Account> UnderwriteAsIndividual(string phoneNumber, string name = null, 
            string dob = null, string streetAddress = null, string city = null, 
            string postalCode = null, string countryCode = null, string email = null,
            Dictionary<string, string> meta = null, string taxId = null);

        /// <summary>
        /// Balanced associates a merchant role to signify whether or not an account has been underwritten.
        /// </summary>
        /// <param name="name">Merchant name. Sequence of characters. Length must be less than or equla to 128.</param>
        /// <param name="phoneNumber"></param>
        /// <param name="emailAdrress"> RFC-2822 formatted email address.</param>
        /// <param name="meta">The meta. Single level mapping from string keys to string values.</param>
        /// <param name="taxId">The tax id. Length must be between 4 and 9.</param>
        /// <param name="dob">Merchant Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="city">The city.</param>
        /// <param name="postalCode">The merchant postal code. Postal code. This is known as a zip code in the USA.</param>
        /// <param name="personDob">Individual Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="personCity">Individual city</param>
        /// <param name="personPostalCode">The individual postal code. Postal code. This is known as a zip code in the USA.</param>
        /// <param name="personStreetAddress">The street address.</param>
        /// <param name="personCountryCode"> ISO-3166-3 three character country code.</param>
        /// <param name="personTaxId">Individual. length must be between 4 and 9.</param>
        /// <returns></returns>
        Status<Account> UnderwriteAsBusiness(string name, string phoneNumber, string emailAddress = null,
            Dictionary<string, string> meta = null, string taxId = null, string dob = null, string city = null, string postalCode = null,
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, string personStreetAddress = null,
            string personCountryCode = null, string personTaxId = null);
    }

    public class AccountClient : IAccountClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public AccountClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Account> Create()
        {
            string url = string.Format("{0}{1}/accounts", 
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);

            return rest.GetResult<Account>(url, this.balanceService.Key, "", "post", null);
        }

        public Status<Account> AddCard(string accountId, string cardId)
        {
            string url = string.Format("{0}{1}/accounts/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            string cardUri = string.Format("{0}/cards/{1}", this.balanceService.MarketplaceUrl, cardId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("card_uri", cardUri);

            return rest.GetResult<Account>(url, this.balanceService.Key, "", "put", parameters);
        }

        public Status<Account> AddBankAccount(string accountId, string bankAccountId)
        {
            string url = string.Format("{0}{1}/accounts/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, accountId);

            string bankAccountUri = string.Format("/v1/bank_accounts/{0}", bankAccountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("bank_account_uri", bankAccountUri);

            return rest.GetResult<Account>(url, this.balanceService.Key, "", "put", parameters);
        }

        public Status<Account> UnderwriteAsIndividual(string phoneNumber, 
            string name = null, string dob = null, string streetAddress = null, 
            string city = null, string postalCode = null, string countryCode = null, 
            string email = null, Dictionary<string, string> meta = null, string taxId = null)
        {
            string url = string.Format("{0}{1}/accounts",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("merchant[phone_number]", phoneNumber);
            parameters.Add("merchant[name]", name);
            parameters.Add("merchant[dob]", dob);
            parameters.Add("merchant[postal_code]", postalCode);
            parameters.Add("merchant[type]", "person");
            parameters.Add("merchant[street_address]", streetAddress);

            return rest.GetResult<Account>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Account> UnderwriteAsBusiness(string name, 
            string phoneNumber, string emailAddress = null, Dictionary<string, string> meta = null, 
            string taxId = null, string dob = null, string city = null, string postalCode = null, 
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, 
            string personStreetAddress = null, string personCountryCode = null, string personTaxId = null)
        {
            string url = string.Format("{0}{1}/accounts",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);
          
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("merchant[phone_number]", phoneNumber);
            parameters.Add("merchant[name]", name);
            parameters.Add("merchant[postal_code]", postalCode);
            parameters.Add("merchant[type]", "business");
            parameters.Add("merchant[street_address]", address);
            parameters.Add("merchant[tax_id]", taxId);
            parameters.Add("merchant[person[dob]]", personDob);
            parameters.Add("merchant[person[postal_code]]", personPostalCode);
            parameters.Add("merchant[person[name]]", personName);
            parameters.Add("merchant[person[street_address]]", personStreetAddress);
            

            return rest.GetResult<Account>(url, this.balanceService.Key, "", "post", parameters);
        }
    }
}
