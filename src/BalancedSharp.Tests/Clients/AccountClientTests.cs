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
    }
}
