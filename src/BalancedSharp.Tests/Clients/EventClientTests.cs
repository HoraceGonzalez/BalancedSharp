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
    public class EventClientTests
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
        //public void Get_Uri()
        //{
        //    string eventId = "EVda9622507c9311e2b21f026ba7cac9da";
        //    this.service.Event.Get(eventId);
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/events/EVda9622507c9311e2b21f026ba7cac9da", this.rest.Uri);            
        //}

        //[Test]
        //public void List_Uri()
        //{
        //    this.service.Event.List();
        //    Assert.AreEqual("https://api.balancedpayments.com/v1/events", this.rest.Uri);    
        //}

        //[Test]
        //public void List_Offset()
        //{
        //    this.service.Event.List(offset: 99);
        //    Assert.AreEqual("99", this.rest.Parameters["offset"]);
        //}
    }
}
