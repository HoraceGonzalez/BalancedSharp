using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Merchant : IBalancedServiceObject
    {
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public Marketplace Marketplace { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string CreatedAt { get; set; }

        public string Uri { get; set; }

        public string AccountsUri { get; set; }

        public Dictionary<string, string> Meta { get; set; }

        public string PostalCode { get; set; }

        public string CountryCode { get; set; }
        
        public int Balance { get; set; }

        public MerchantType Type { get; set; }

        public string Id { get; set; }

        public string StreetAddress { get; set; }

        public string ApiKeysUri { get; set; }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
