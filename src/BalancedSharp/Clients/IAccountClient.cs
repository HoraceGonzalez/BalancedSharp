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
        /// <param name="name">Merchant name. Length must be less than or equla to 128.</param>
        /// <param name="phoneNumber">Merchant phone number.</param>
        /// <param name="emailAdrress">Merchant email address.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="taxId">Merchant tax id. Length must be between 4 and 9.</param>
        /// <param name="dob">Merchant Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="city">Merchant city.</param>
        /// <param name="postalCode">Merchant postal code. This is known as a zip code in the USA.</param>
        /// <param name="personDob">Individual Date-of-birth formatted as YYYY-MM-DD.</param>
        /// <param name="personCity">Individual city.</param>
        /// <param name="personPostalCode">Individual postal code. This is known as a zip code in the USA.</param>
        /// <param name="personStreetAddress">Individual street address.</param>
        /// <param name="personCountryCode">Individual three character country code.</param>
        /// <param name="personTaxId">Individual tax id. Length must be between 4 and 9.</param>
        /// <returns></returns>
        Status<Account> UnderwriteAsBusiness(string name, string phoneNumber, string emailAddress = null,
            Dictionary<string, string> meta = null, string taxId = null, string dob = null, string city = null, string postalCode = null,
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, string personStreetAddress = null,
            string personCountryCode = null, string personTaxId = null);
        
        /// <summary>
        /// Retrieves the details of a bank account that has previously been created. 
        /// The same information is returned when creating the bank account.
        /// </summary>
        /// <param name="accountId">The account uri.</param>
        /// <returns>BankAccount details</returns>
        Status<Account> Get(string accountUri);

        /// <summary>
        /// Debits an account.
        /// Successful creation of a debit using a card will
        /// return an associated hold mapping as part of the response.
        /// This hold was created and captured behind the scenes automatically. 
        /// For ACH debits there is no corresponding hold.
        /// </summary>
        /// <param name="accountUri">The account uri.</param>
        /// <param name="amount">If the resolving URI references a hold then this is hold amount. You can always capture less than the hold amount (e.g. a partial capture). Otherwise its the maximum per debit amount for your marketplace. Value must be greater than or equal to the minimum per debit amount for your marketplace. Value must be less than or equal to the maximum per debit amount for marketplace.</param>
        /// <param name="appearsOnStatementAs">Text that will appear on the buyer's statement.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="description">The description.</param>        
        /// <param name="onBehalfOfUri">The account of a merchant, usually a seller or service provider, that is associated with this card charge or bank account debit.</param>
        /// <param name="holdUri">If no hold is provided one my be generated and captured if the funding source is a card.</param>
        /// <param name="sourceUri">URI of a specific bank account or card to be debited.</param>
        /// <returns>Debit details</returns>
        Status<Debit> Debit(string bankAccountUri, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null);
    }

    public class AccountClient : IAccountClient
    {
        IBalancedRest rest;

        public AccountClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Account> Create()
        {
            string url = string.Format("{0}{1}/accounts", 
                this.Service.BaseUri, this.Service.MarketplaceUrl);

            return rest.GetResult<Account>(url, this.Service.Key, "", "post", null);
        }

        public Status<Account> AddCard(string accountId, string cardId)
        {
            string url = string.Format("{0}{1}/accounts/{2}",
                this.Service.BaseUri, this.Service.MarketplaceUrl, accountId);

            string cardUri = string.Format("{0}/cards/{1}", this.Service.MarketplaceUrl, cardId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("card_uri", cardUri);

            return rest.GetResult<Account>(url, this.Service.Key, "", "put", parameters);
        }

        public Status<Account> AddBankAccount(string accountId, string bankAccountId)
        {
            string url = string.Format("{0}{1}/accounts/{2}",
                this.Service.BaseUri, this.Service.MarketplaceUrl, accountId);

            string bankAccountUri = string.Format("/v1/bank_accounts/{0}", bankAccountId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("bank_account_uri", bankAccountUri);

            return rest.GetResult<Account>(url, this.Service.Key, "", "put", parameters);
        }

        public Status<Account> UnderwriteAsIndividual(string phoneNumber, 
            string name = null, string dob = null, string streetAddress = null, 
            string city = null, string postalCode = null, string countryCode = null, 
            string email = null, Dictionary<string, string> meta = null, string taxId = null)
        {
            string url = string.Format("{0}{1}/accounts",
                this.Service.BaseUri, this.Service.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("merchant[type]", "person");
            parameters.Add("merchant[phone_number]", phoneNumber);
            parameters.Add("merchant[name]", name);
            parameters.Add("merchant[dob]", dob);
            parameters.Add("merchant[street_address]", streetAddress);
            parameters.Add("merchant[city]", city);
            parameters.Add("merchant[postal_code]", postalCode);
            parameters.Add("merchant[country_code]", countryCode);
            parameters.Add("merchant[email]", email);
            parameters.Add("merchant[tax_id]", taxId);

            return rest.GetResult<Account>(url, this.Service.Key, "", "post", parameters);
        }

        public Status<Account> UnderwriteAsBusiness(string name, 
            string phoneNumber, string emailAddress = null, Dictionary<string, string> meta = null, 
            string taxId = null, string dob = null, string city = null, string postalCode = null, 
            string countryCode = null, string address = null, string personName = null,
            string personDob = null, string personCity = null, string personPostalCode = null, 
            string personStreetAddress = null, string personCountryCode = null, string personTaxId = null)
        {
            string url = string.Format("{0}{1}/accounts",
                this.Service.BaseUri, this.Service.MarketplaceUrl);
          
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
            parameters.Add("merchant[person[street_address]]", personStreetAddress);
            parameters.Add("merchant[person[country_code]]", personCountryCode);
            parameters.Add("merchant[person[tax_id]]", personTaxId);

            return rest.GetResult<Account>(url, this.Service.Key, "", "post", parameters);
        }

        public Status<Account> Get(string accountUri)
        {
            string url = string.Format("{0}{1}", this.Service.BaseUri, accountUri);
            return this.rest.GetResult<Account>(url, this.Service.Key, null, "get", null);
        }

        public Status<Debit> Debit(string accountUri, int? amount = null,
            string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
            string onBehalfOfUri = null, string holdUri = null, string sourceUri = null)
        {
            string url = string.Format("{0}/v1/{1}accounts/{2}/debits", this.Service.BaseUri,
                this.Service.MarketplaceUrl, accountUri);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (amount.HasValue) parameters.Add("amount", amount.Value.ToString());
            parameters.Add("appears_on_statement_as", appearsOnStatementAs);
            parameters.Add("description", description);
            parameters.Add("account_uri", accountUri);
            parameters.Add("on_behalf_of_uri", onBehalfOfUri);
            parameters.Add("hold_uri", holdUri);
            parameters.Add("source_uri", sourceUri);

            return rest.GetResult<Debit>(url, this.Service.Key, "", "post", parameters);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
