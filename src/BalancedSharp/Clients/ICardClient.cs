using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// You'll eventually want to be able to charge credit/debit cards
    /// without having to ask your users for their information over
    /// and over again. To do this, you'll need to create a card resource.
    /// </summary>
    public interface ICardClient
    {
        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="cardNumber">The digits of the credit card number.</param>
        /// <param name="expirationYear">Expiration year. The current year or later. Value must be less than or equal to 9999.</param>
        /// <param name="expirationMonth">Expiration month (e.g. 1 for January). Value must be less than or equal to 12.</param>
        /// <param name="securityCode">The 3-4 digit security code for the card.</param>
        /// <param name="name">Sequence of characters. Length must be less than or equal to 128.</param>
        /// <param name="phoneNumber">E.164 formatted phone number. Length must be less than or equal to 15.</param>
        /// <param name="city">City.</param>
        /// <param name="postalCode">Postal code. This is known as a zip code in the USA.</param>
        /// <param name="streetAddress">Street address.</param>
        /// <param name="countryCode">ISO-3166-3 three character country code.</param>
        /// <param name="meta">Single level mapping from string keys to string values.</param>
        /// <param name="isValid">Indicates whether the card is active (true) or has been deactivated (false).</param>
        /// <returns>Card details</returns>
        Status<Card> Tokenize(string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null,
            string city = null, string postalCode = null, string streetAddress = null, string countryCode = null,
            Dictionary<string, string> meta = null, bool isValid = true);

        /// <summary>
        /// Retrieves the details of a card that has previously been created.
        /// The same information is returned when creating the card.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <returns>Card details</returns>
        Status<Card> Get(string cardId);

        /// <summary>
        /// Returns a list of cards that you've created.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of card details</returns>
        Status<PagedList<Card>> List(int limit = 10, int offset = 0);

        /// <summary>
        /// Update information in a card
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <returns>Card details</returns>
        Status<Card> Update(string cardId);

        /// <summary>
        /// Invalidating a card will mark the card as invalid, so
        /// it may not be charged.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <returns>Card details</returns>
        Status<Card> Invalidate(string cardId);
    }

    public class CardClient : ICardClient
    {
        IBalancedService balanceService;
        IBalancedRest rest;

        public CardClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.balanceService = balanceService;
            this.rest = rest;
        }

        public Status<Card> Tokenize(string cardNumber, int expirationYear, 
            int expirationMonth, string securityCode = null, string name = null, 
            string phoneNumber = null, string city = null, string postalCode = null, 
            string streetAddress = null, string countryCode = null, Dictionary<string, string> meta = null, bool isValid = true)
        {
            string url = string.Format("{0}{1}/cards",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("card_number", cardNumber);
            parameters.Add("expiration_year", expirationYear.ToString());
            parameters.Add("expiration_month", expirationMonth.ToString());
            parameters.Add("security_code", securityCode);
            parameters.Add("name", name);
            parameters.Add("phone_number", phoneNumber);
            parameters.Add("city", city);
            parameters.Add("postal_code", postalCode);
            parameters.Add("street_address", streetAddress);
            parameters.Add("country_code", countryCode);
            parameters.Add("is_valid", isValid.ToString().ToLower());

            return rest.GetResult<Card>(url, this.balanceService.Key, "", "post", parameters);
        }

        public Status<Card> Get(string cardId)
        {
            string url = string.Format("{0}{1}/cards/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, cardId);

            return rest.GetResult<Card>(url, this.balanceService.Key, "", "get", null);
        }

        public Status<PagedList<Card>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/cards",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Card>>(url, this.balanceService.Key, "", "get", parameters);
        }

        public Status<Card> Update(string cardId)
        {
            string url = string.Format("{0}{1}/cards/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, cardId);

            Dictionary<string, string> parameters = new Dictionary<string,string>();
            // no documentation on params

            return rest.GetResult<Card>(url, this.balanceService.Key, "", "put", parameters);
        }

        public Status<Card> Invalidate(string cardId)
        {
            string url = string.Format("{0}{1}/cards/{2}",
                this.balanceService.BaseUri, this.balanceService.MarketplaceUrl, cardId);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("is_valid", "false");

            return rest.GetResult<Card>(url, this.balanceService.Key, "", "put", parameters);
        }
    }
}
