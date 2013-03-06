using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class HoldClientTests
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

        //[Test]
        //public void New_Uri()
        //{
        //    this.service.Hold.New("AC3z0Z98UsRL1DERqFlb9wu", 9000);
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/holds", this.rest.Uri);
        //}

        //[Test]
        //public void New_Amount()
        //{
        //    this.service.Hold.New("AC3z0Z98UsRL1DERqFlb9wu", 9000);
        //    Assert.AreEqual("9000", this.rest.Parameters["amount"]);
        //}

        //[Test]
        //public void Get_Uri()
        //{
        //    this.service.Hold.Get("HLSHkqjLpDzRaslgk3Y02Pe");
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HLSHkqjLpDzRaslgk3Y02Pe", this.rest.Uri);
        //}

        //[Test]
        //public void List_Uri()
        //{
        //    this.service.Hold.List();
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds", this.rest.Uri);
        //}

        //[Test]
        //public void List_Offset()
        //{
        //    this.service.Hold.List(offset: 5);
        //    Assert.AreEqual("5", this.rest.Parameters["offset"]);
        //}

        //[Test]
        //public void ListAccount_Uri()
        //{
        //    this.service.Hold.List("AC3z0Z98UsRL1DERqFlb9wu");
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/holds", this.rest.Uri);
        //}

        //[Test]
        //public void ListAccount_Limit()
        //{
        //    this.service.Hold.List("AC3z0Z98UsRL1DERqFlb9wu", limit: 25);
        //    Assert.AreEqual("25", this.rest.Parameters["limit"]);
        //}

        //[Test]
        //public void Update_Uri()
        //{
        //    this.service.Hold.Update("HLSHkqjLpDzRaslgk3Y02Pe");
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HLSHkqjLpDzRaslgk3Y02Pe", this.rest.Uri);
        //}

        //[Test]
        //public void Update_IsVoid()
        //{
        //    this.service.Hold.Update("HLSHkqjLpDzRaslgk3Y02Pe", isVoid: true);
        //    Assert.AreEqual("true", this.rest.Parameters["is_void"]);
        //}

        //[Test]
        //public void Capture_Uri()
        //{
        //    this.service.Hold.Capture("AC3z0Z98UsRL1DERqFlb9wu", "HLZNKOsVAfHkmmsknB4zcOi");
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/debits", this.rest.Uri);
        //}

        //[Test]
        //public void Capture_HoldUri()
        //{
        //    this.service.Hold.Capture("AC3z0Z98UsRL1DERqFlb9wu", "HLZNKOsVAfHkmmsknB4zcOi");
        //    Assert.AreEqual("/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HLZNKOsVAfHkmmsknB4zcOi", this.rest.Parameters["hold_uri"]);
        //}

        //[Test]
        //public void Void_Uri()
        //{
        //    this.service.Hold.Void("HL15Hrl0Wi84k94VFtI1hr6E");
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HL15Hrl0Wi84k94VFtI1hr6E", this.rest.Uri);
        //}

        //[Test]
        //public void Void_IsVoid()
        //{
        //    this.service.Hold.Void("HL15Hrl0Wi84k94VFtI1hr6E");
        //    Assert.AreEqual("true", this.rest.Parameters["is_void"]);
        //}
    }
}
