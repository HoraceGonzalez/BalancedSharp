using BalancedSharp.Clients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class AccountClientTests
    {
        BalancedService service;
        FakeRest rest;

        [SetUp]
        public void Setup()
        {
            this.rest = new FakeRest();
            this.service = new BalancedService(Config.ApiKey, this.rest);
        }

        [Test]
        public void AddCard_Params()
        {
            string accountsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC7D2TvkfsxPmc8VpYQrdmhi";
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7FHSFn8oRlSJSAjiBpRtLa";

            this.service.Account.AddCard(accountsUri, cardUri);
            Assert.AreEqual(cardUri, this.rest.Parameters["card_uri"]);
        }

        [Test]
        public void AddBankAccount_Params()
        {
            string accountsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu";
            string bankAccountUri = "/v1/bank_accounts/BA7IU7MDWuhYHjlDID0WVXG";

            this.service.Account.AddBankAccount(accountsUri, bankAccountUri);
            Assert.AreEqual(bankAccountUri, this.rest.Parameters["bank_account_uri"]);
        }

        [Test]
        public void UnderwriteIndividual_Params()
        {
            string accountsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts";
            string type = "person";
            string phoneNumber = "+14089999999";
            string dob = "1989-12";
            string postalCode = "94110";
            string name = "Timmy Q. CopyPasta";
            string address = "121 Skriptkid Row";

            this.service.Account.UnderwriteAsIndividual(accountsUri, phoneNumber, dob: dob, postalCode: postalCode, name: name, address: address);
            Assert.AreEqual(type, this.rest.Parameters["merchant[type]"]);
            Assert.AreEqual(phoneNumber, this.rest.Parameters["merchant[phone_number]"]);
            Assert.AreEqual(dob, this.rest.Parameters["merchant[dob]"]);
            Assert.AreEqual(postalCode, this.rest.Parameters["merchant[postal_code]"]);
            Assert.AreEqual(name, this.rest.Parameters["merchant[name]"]);
            Assert.AreEqual(address, this.rest.Parameters["merchant[street_address]"]);
        }

        [Test]
        public void UnderwriteAsBusiness_Params()
        {
            string accountsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts";
            string phoneNumber = "140899188155";
            string name = "Skripts4Kids";
            string postalCode = "91111";
            string type = "business";
            string address = "555 VoidMain Road";
            string taxId = "211111111";
            string dob = "1982-12";
            string personPostalCode = "94110";
            string personName = "Timmy Q. CopyPasta";
            string personAddress = "121 Skriptkid Row";
            this.service.Account.UnderwriteAsBusiness(accountsUri, name, phoneNumber, postalCode: postalCode, address: address, taxId: taxId, dob: dob,
                personPostalCode: personPostalCode, personName: personName, personAddress: personAddress);
            Assert.AreEqual(phoneNumber, this.rest.Parameters["merchant[phone_number]"]);
            Assert.AreEqual(name, this.rest.Parameters["merchant[name]"]);
            Assert.AreEqual(postalCode, this.rest.Parameters["merchant[postal_code]"]);
            Assert.AreEqual(type, this.rest.Parameters["merchant[type]"]);
            Assert.AreEqual(address, this.rest.Parameters["merchant[street_address]"]);
            Assert.AreEqual(taxId, this.rest.Parameters["merchant[tax_id]"]);
            Assert.AreEqual(dob, this.rest.Parameters["merchant[dob]"]);
            Assert.AreEqual(personPostalCode, this.rest.Parameters["merchant[person[postal_code]]"]);
            Assert.AreEqual(personName, this.rest.Parameters["merchant[person[name]]"]);
            Assert.AreEqual(personAddress, this.rest.Parameters["merchant[person[street_address]]"]);
        }
    }
}
