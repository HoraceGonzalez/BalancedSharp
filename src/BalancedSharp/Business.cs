using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public class Business
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public string TaxId { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string CountryCode { get; set; }
        public Person Person { get; set; }

        public Business()
        {
        }

        public Business(string name, string phoneNumber, string postalCode, string streetAddress, string taxId, Person person)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.PostalCode = postalCode;
            this.StreetAddress = streetAddress;
            this.TaxId = taxId;
            this.Person = person;
        }
    }
}
