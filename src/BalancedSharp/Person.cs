using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public class Person
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Meta { get; set; }
        public string TaxId { get; set; }
        public string DateOfBirth { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
        public string CountryCode { get; set; }

        public Person()
        {
        }

        public Person(string name, string dateOfBirth)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
        }

        public Person(string name, string dateOfBirth, string phoneNumber, string streetAddress, string postalCode)
        {
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.PhoneNumber = phoneNumber;
            this.StreetAddress = streetAddress;
            this.PostalCode = postalCode;
        }
    }
}
