using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICardClient
    {
        Status<Card> Tokenize(string marketplaceId, string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null,
            string city = null, string postalCode = null, string streetAddress = null, string countryCode = null,
            string meta = null, bool isValid = true);

        Status<Card> Get(string marketplaceId, string cardId);

        Status<Card> List(string marketplaceId, int limit = 10, int offset = 0);

        Status<Card> Update(string marketplaceId, string cardId);

        Status<Card> Invalidate(string marketplaceId, string cardId);
    }

    public class CardClient : ICardClient
    {
        IBalancedService balanceService;

        public CardClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Card> Tokenize(string marketplaceId, string cardNumber, int expirationYear, 
            int expirationMonth, string securityCode = null, string name = null, 
            string phoneNumber = null, string city = null, string postalCode = null, 
            string streetAddress = null, string countryCode = null, string meta = null, bool isValid = true)
        {
            throw new NotImplementedException();
        }

        public Status<Card> Get(string marketplaceId, string cardId)
        {
            throw new NotImplementedException();
        }

        public Status<Card> List(string marketplaceId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Card> Update(string marketplaceId, string cardId)
        {
            throw new NotImplementedException();
        }

        public Status<Card> Invalidate(string marketplaceId, string cardId)
        {
            throw new NotImplementedException();
        }
    }
}
