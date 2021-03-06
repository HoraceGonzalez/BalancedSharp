﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Card : IBalancedServiceObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "brand")]
        public string Brand { get; set; }

        [DataMember(Name = "can_debit")]
        public bool CanDebit { get; set; }

        [DataMember(Name = "card_type")]
        public string CardType { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedOn { get; set; }

        [DataMember(Name = "expiration_month")]
        public int ExpirationMonth { get; set; }

        [DataMember(Name = "expiration_year")]
        public int ExpirationYear { get; set; }

        [DataMember(Name = "hash")]
        public string Hash { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "is_valid")]
        public bool IsValid { get; set; }

        [DataMember(Name = "last_four")]
        public int LastFourDigits { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string,string> Meta { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public string CardNumber { get; set; }

        public string SecurityCode { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string StreetAddress { get; set; }

        public string CountryCode { get; set; }

        public Card()
        {
        }

        public Card(string cardNumber, int expYear, int expMonth)
            :base()
        {
            this.CardNumber = cardNumber;
            this.ExpirationYear = expYear;
            this.ExpirationMonth = expMonth;
        }

        public Status<Card> Update()
        {
            return this.Service.Card.Update(Uri, Meta);
        }

        public Status<Card> Invalidate()
        {
            return this.Service.Card.Invalidate(Uri);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
