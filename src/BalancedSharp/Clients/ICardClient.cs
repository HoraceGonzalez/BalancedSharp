using System.Collections.Generic;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// You'll eventually want to be able to charge credit/debit cards
    /// without having to ask your users for their information over
    /// and over again. To do this, you'll need to create a card resource.
    /// </summary>
    public interface ICardClient : IBalancedServiceObject
    {
        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="cardUri">The carda uri.</param>
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
        Status<Card> Create(string cardsUri, string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null, string city = null,
            string postalCode = null, string streetAddress = null, string countryCode = null,
            Dictionary<string, string> meta = null, bool isValid = true);

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
        /// <param name="cardsUri">The cards uri.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>PagedList of card details</returns>
        Status<PagedList<Card>> List(string cardsUri, int limit = 10, int offset = 0);

        /// <summary>
        /// Update information in a card.
        /// </summary>
        /// <param name="cardUri">The carda uri.</param>
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
        /// <returns>Updated card details</returns>
        Status<Card> Update(string cardsUri, Dictionary<string, string> meta);

        /// <summary>
        /// Invalidating a card will mark the card as invalid, so it may not be charged.
        /// </summary>
        /// <param name="cardUri">The cards uri.</param>
        /// <returns>Card details</returns>
        Status<Card> Invalidate(string cardUri);
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

        public Status<Card> Create(string cardsUri, string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null, string city = null,
            string postalCode = null, string streetAddress = null, string countryCode = null,
            Dictionary<string, string> meta = null, bool isValid = true)
        {
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
            if (meta != null)
                foreach (var key in meta.Keys)
                    parameters.Add(string.Format("meta[{0}]", key), meta[key]);
            return this.rest.GetResult<Card>(this.Service.BaseUrl + cardsUri, this.Service.Key, null, "post", parameters).AttachService(this.Service);
        }

        public Status<Card> Get(string cardsUri)
        {
            return this.rest.GetResult<Card>(this.Service.BaseUrl + cardsUri, this.Service.Key, null, "get", null).AttachService(this.Service);
        }

        public Status<PagedList<Card>> List(string cardsUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            return this.rest.GetResult<PagedList<Card>>(this.Service.BaseUrl + cardsUri, this.Service.Key, null, "get", parameters).AttachService(this.Service);
        }

        public Status<Card> Update(string cardsUri, Dictionary<string, string> meta)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (meta != null)
                foreach (var key in meta.Keys)
                    parameters.Add(string.Format("meta[{0}]", key), meta[key]);
            return this.rest.GetResult<Card>(this.Service.BaseUrl + cardsUri, this.Service.Key, null, "put", parameters).AttachService(this.Service);
        }

        public Status<Card> Invalidate(string cardsUri)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("is_valid", "false");
            return this.rest.GetResult<Card>(this.Service.BaseUrl + cardsUri, this.Service.Key, null, "put", parameters).AttachService(this.Service);
        }
    }
}
