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
            this.service = new BalancedService(
                "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ", 
                Config.ApiKey, this.rest);
        }

        [Test]
        public void AddCard_Uri()
        {
            this.service.Account.AddCard("AC7D2TvkfsxPmc8VpYQrdmhi", "CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC7D2TvkfsxPmc8VpYQrdmhi");
        }

        [Test]
        public void AddCard_CardUri()
        {
            string card = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7FHSFn8oRlSJSAjiBpRtLa";
            this.service.Account.AddCard("AC7D2TvkfsxPmc8VpYQrdmhi ", "CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual(this.rest.Parameters["card_uri"], card);
        }

        [Test]
        public void AddBankAccount_Uri()
        {
            this.service.Account.AddBankAccount("AC3z0Z98UsRL1DERqFlb9wu", "BA7IU7MDWuhYHjlDID0WVXG");
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu");
        }

        [Test]
        public void AddBankAccount_BankAccountUri()
        {
            string bank = "/v1/bank_accounts/BA7IU7MDWuhYHjlDID0WVXG";
            this.service.Account.AddBankAccount("AC7D2TvkfsxPmc8VpYQrdmhi", "BA7IU7MDWuhYHjlDID0WVXG");
            Assert.AreEqual(this.rest.Parameters["bank_account_uri"], bank);
        }

        [Test]
        public void UnderwriteAsIndividual_Params()
        {
            string phone_number = "14089999999";
            string name = "Timmy Q. CopyPasta";
            string dob = "1989-12";
            string postal_code = "94110";
            string type = "person";
            string street_address = "121 Skriptkid Row";
            this.service.Account.UnderwriteAsIndividual("14089999999", "Timmy Q. CopyPasta", "1989-12",
                "121 Skriptkid Row", null, "94110", null, null, null, null );
            Assert.AreEqual(this.rest.Parameters["merchant[phone_number]"], phone_number);
            Assert.AreEqual(this.rest.Parameters["merchant[name]"], name);
            Assert.AreEqual(this.rest.Parameters["merchant[dob]"], dob);
            Assert.AreEqual(this.rest.Parameters["merchant[postal_code]"], postal_code);
            Assert.AreEqual(this.rest.Parameters["merchant[type]"], type);
            Assert.AreEqual(this.rest.Parameters["merchant[street_address]"], street_address);
        }

        [Test]
        public void UnderwriteAsBusiness_Params()
        {
            string phone_number = "140899188155";
            string name = "Skripts4Kids";            
            string postal_code = "91111";
            string type = "business";
            string street_address = "555 VoidMain Road";
            string tax_id = "211111111";            
            string dob = "1982-12";
            string person_postal_code = "94110";
            string person_name = "Timmy Q. CopyPasta";
            string person_street_address = "121 Skriptkid Row";
            this.service.Account.UnderwriteAsBusiness("Skripts4Kids", "140899188155", null, null,
                "211111111", "null", null, "91111", null, "555 VoidMain Road", 
                "Timmy Q. CopyPasta", "1982-12", null, "94110", "121 Skriptkid Row", null, null);
            Assert.AreEqual(this.rest.Parameters["merchant[phone_number]"], phone_number);
            Assert.AreEqual(this.rest.Parameters["merchant[name]"], name);
            Assert.AreEqual(this.rest.Parameters["merchant[postal_code]"], postal_code);
            Assert.AreEqual(this.rest.Parameters["merchant[type]"], type);
            Assert.AreEqual(this.rest.Parameters["merchant[street_address]"], street_address);
            Assert.AreEqual(this.rest.Parameters["merchant[tax_id]"], tax_id);
            Assert.AreEqual(this.rest.Parameters["merchant[person[dob]]"], dob);
            Assert.AreEqual(this.rest.Parameters["merchant[person[postal_code]]"], person_postal_code);
            Assert.AreEqual(this.rest.Parameters["merchant[person[name]]"], person_name);
            Assert.AreEqual(this.rest.Parameters["merchant[person[street_address]]"], person_street_address);
            
        }

    }
}
