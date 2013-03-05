using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class CreditClientTests
    {
        BalancedService service;
        FakeRest rest;

        [SetUp]
        public void Setup()
        {
            this.rest = new FakeRest();
            this.service = new BalancedService(
                Config.MarketplaceUri,
                Config.ApiKey, this.rest);
        }

        [Test]
        public void CreditNewBank_Uri()
        {
            this.service.Credit.New(1000, "Tony Stark", "9900000001", "999999999", "121000358", BankAccountType.Checking);
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits", this.rest.Uri);
        }

        [Test]
        public void CreditNewBank_AccountNumber()
        {
            this.service.Credit.New(1000, "Bruce Wayne", "1234567891", "999999999", "121000358", BankAccountType.Checking);
            Assert.AreEqual("1234567891", this.rest.Parameters["account_number"]);
        }

        [Test]
        public void CreditExistingBank_Uri()
        {
            this.service.Credit.New(2500, "BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs/credits", this.rest.Uri);
        }

        [Test]
        public void CreditExistingBank_Amount()
        {
            this.service.Credit.New(9000, "BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual("9000", this.rest.Parameters["amount"]);
        }

        [Test]
        public void CreditAccount_Uri()
        {
            this.service.Credit.New("AC3z0Z98UsRL1DERqFlb9wu", 4500);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/credits", this.rest.Uri);
        }

        [Test]
        public void CreditAccount_Amount()
        {
            this.service.Credit.New("AC3z0Z98UsRL1DERqFlb9wu", 4321);
            Assert.AreEqual("4321", this.rest.Parameters["amount"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Credit.Get("CRfZww6HK7lPyCX5WruUx3G");
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits/CRfZww6HK7lPyCX5WruUx3G", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Credit.List();
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits", this.rest.Uri);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Credit.List(limit: 25);
            Assert.AreEqual("25", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Credit.List(offset: 30);
            Assert.AreEqual("30", this.rest.Parameters["offset"]);
        }

        [Test]
        public void ListBank_Uri()
        {
            this.service.Credit.List("BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs/credits", this.rest.Uri);
        }

        [Test]
        public void ListBank_Limit()
        {
            this.service.Credit.List("BA6ThbEt9vlVXtNB1K4C9VUs", limit: 90);
            Assert.AreEqual("90", this.rest.Parameters["limit"]);
        }

        [Test]
        public void ListBank_Offset()
        {
            this.service.Credit.List("BA6ThbEt9vlVXtNB1K4C9VUs", offset: 7);
            Assert.AreEqual("7", this.rest.Parameters["offset"]);
        }
    }
}
