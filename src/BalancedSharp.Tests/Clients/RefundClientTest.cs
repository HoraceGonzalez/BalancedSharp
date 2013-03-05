using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class RefundClientTest
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
        public void New_Uri()
        {
            string accountId = "AC3z0Z98UsRL1DERqFlb9wu";
            this.service.Refund.New("", accountId);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/refunds", this.rest.Uri);    
        }

        [Test]
        public void New_DebitUri()
        {
            string debitId = "WD1aQpr85FhzCCccOI1UnGes";
            this.service.Refund.New(debitId, "");
            Assert.AreEqual("/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/debits/WD1aQpr85FhzCCccOI1UnGes", this.rest.Parameters["debit_uri"]);
        }

        [Test]
        public void New_Amount()
        {
            this.service.Refund.New("", "", 100);
            Assert.AreEqual("100", this.rest.Parameters["amount"]);
        }

        [Test]
        public void New_Description()
        {
            this.service.Refund.New("", "", 0, "This is a test");
            Assert.AreEqual("This is a test", this.rest.Parameters["description"]);
        }

        [Test]
        public void Get_Uri()
        {
            string refundId = "RF1bNMx3J48PAiYNJMga00YE";
            this.service.Refund.Get(refundId);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/refunds/RF1bNMx3J48PAiYNJMga00YE", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Refund.List();
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/refunds", this.rest.Uri);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Refund.List(5);
            Assert.AreEqual("5", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Refund.List(5, 10);
            Assert.AreEqual("10", this.rest.Parameters["offset"]);
        }

        [Test]
        public void List_AccountUri()
        {
            string accountId = "AC3z0Z98UsRL1DERqFlb9wu";
            this.service.Refund.List(accountId);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/refunds", this.rest.Uri);
        }

        [Test]
        public void Update_Uri() 
        {
            string refundId = "RF1bNMx3J48PAiYNJMga00YE";
            this.service.Refund.Update(refundId);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/refunds/RF1bNMx3J48PAiYNJMga00YE", this.rest.Uri);
        }

        [Test]
        public void Update_Description()
        {
            this.service.Refund.Update("", "this is a test");
            Assert.AreEqual("this is a test", this.rest.Parameters["description"]);
        }
    }
}
