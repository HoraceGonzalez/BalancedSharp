using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class DebitClientTests
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
        public void New_Uri()
        {
            this.service.Debit.New("AC3z0Z98UsRL1DERqFlb9wu");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/debits", this.rest.Uri);
        }

        [Test]
        public void New_Amount()
        {
            this.service.Debit.New("AC3z0Z98UsRL1DERqFlb9wu", 9000);
            Assert.AreEqual("9000", this.rest.Parameters["amount"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Debit.Get("WDEBPPEakDQzIE6T5YVjKC4");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/debits/WDEBPPEakDQzIE6T5YVjKC4", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Debit.List();
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/debits", this.rest.Uri);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Debit.List(limit: 9);
            Assert.AreEqual("9", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Debit.List(offset: 3);
            Assert.AreEqual("3", this.rest.Parameters["offset"]);
        }

        [Test]
        public void ListAccount_Uri()
        {
            this.service.Debit.List("AC3z0Z98UsRL1DERqFlb9wu");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/debits", this.rest.Uri);
        }

        [Test]
        public void ListAccount_Limit()
        {
            this.service.Debit.List("AC3z0Z98UsRL1DERqFlb9wu", limit: 71);
            Assert.AreEqual("71", this.rest.Parameters["limit"]);
        }

        [Test]
        public void ListAccount_Offset()
        {
            this.service.Debit.List("AC3z0Z98UsRL1DERqFlb9wu", offset: 49);
            Assert.AreEqual("49", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Update_Uri()
        {
            this.service.Debit.Update("AC3z0Z98UsRL1DERqFlb9wu", "WDEBPPEakDQzIE6T5YVjKC4");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/debits/WDEBPPEakDQzIE6T5YVjKC4", this.rest.Uri);
        }

        [Test]
        public void Update_Description()
        {
            this.service.Debit.Update("AC3z0Z98UsRL1DERqFlb9wu", "WDEBPPEakDQzIE6T5YVjKC4", description: "Hello I am a description");
            Assert.AreEqual("Hello I am a description", this.rest.Parameters["description"]);
        }
    }
}
