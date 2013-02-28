using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface ICardClient
    {
        Status<Card> Tokenize(string cardNumber, int expirationYear, int expirationMonth,
            string securityCode = null, string name = null, string phoneNumber = null,
            string city = null, string postalCode = null, string streetAddress = null, string countryCode = null,
            Dictionary<string, string> meta = null, bool isValid = true);

        Status<Card> Get(string cardId);

        Status<Card> List(int limit = 10, int offset = 0);

        Status<Card> Update(string cardId);

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
            throw new NotImplementedException();
        }

        public Status<Card> Get(string cardId)
        {
            throw new NotImplementedException();
        }

        public Status<Card> List(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Card> Update(string cardId)
        {
            throw new NotImplementedException();
        }

        public Status<Card> Invalidate(string cardId)
        {
            throw new NotImplementedException();
        }
    }
}
