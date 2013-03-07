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
    public interface ICardClient : IBalancedServiceObject
    {
        // TODO

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="cardUri">The card uri.</param>
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
        Card New(string cardUri, string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null,
            string city = null, string postalCode = null, string streetAddress = null, string countryCode = null,
            Dictionary<string, string> meta = null, bool isValid = true);

        Status<Card> Save(Card card, string requestMethod);

        /// <summary>
        /// Retrieves the details of a card that has previously been created.
        /// The same information is returned when creating the card.
        /// </summary>
        /// <param name="cardUri">The card uri.</param>
        /// <returns>Card details</returns>
        Status<Card> Get(string cardUri);

        /// <summary>
        /// Returns a list of cards that you've created.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of card details</returns>
        Status<PagedList<Card>> List(int limit = 10, int offset = 0);
    }

    public class CardClient : ICardClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public CardClient(IBalancedService balanceService, IBalancedRest rest)
        {
            Service = balanceService;
            this.rest = rest;
        }

        public Card New(string cardUri, string cardNumber, int expirationYear, 
            int expirationMonth, string securityCode = null, string name = null, 
            string phoneNumber = null, string city = null, string postalCode = null, 
            string streetAddress = null, string countryCode = null, Dictionary<string, string> meta = null, bool isValid = true)
        {
            return new Card
            {
                Uri = cardUri,
                CardNumber = cardNumber,
                ExpirationYear = expirationYear,
                ExpirationMonth = expirationMonth,
                SecurityCode = securityCode,
                Name = name,
                PhoneNumber = phoneNumber,
                City = city,
                PostalCode = postalCode,
                StreetAddress = streetAddress,
                CountryCode = countryCode,
                Meta = meta,
                IsValid = isValid,
                Service = this.Service
            };
        }

        public Status<Card> Save(Card card, string requestMethod)
        {
            string url = string.Format("{0}{1}", this.Service.BaseUri, card.Uri);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("card_number", card.CardNumber);
            parameters.Add("expiration_year", card.ExpirationYear.ToString());
            parameters.Add("expiration_month", card.ExpirationMonth.ToString());
            parameters.Add("security_code", card.SecurityCode);
            parameters.Add("name", card.Name);
            parameters.Add("phone_number", card.PhoneNumber);
            parameters.Add("city", card.City);
            parameters.Add("postal_code", card.PostalCode);
            parameters.Add("street_address", card.StreetAddress);
            parameters.Add("country_code", card.CountryCode);
            parameters.Add("is_valid", card.IsValid.ToString().ToLower());
            return this.rest.GetResult<Card>(url, this.Service.Key, null, requestMethod, parameters);
        }

        public Status<Card> Get(string cardUri)
        {
            string url = string.Format("{0}{1}", this.Service.BaseUri, cardUri);
            return this.rest.GetResult<Card>(url, this.Service.Key, null, "get", null);
        }

        public Status<PagedList<Card>> List(int limit = 10, int offset = 0)
        {
            string url = string.Format("{0}{1}/cards",
                this.Service.BaseUri, this.Service.MarketplaceUrl);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Card>>(url, this.Service.Key, "", "get", parameters);
        }
    }
}
