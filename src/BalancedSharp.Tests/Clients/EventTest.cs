using BalancedSharp.Clients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    class EventTest
    {
        BalancedService service;
        FakeRest rest;

        [SetUp]
        public void Setup()
        {
            this.rest = new FakeRest();
            this.service = new BalancedService(
                "/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ",
                Config.ApiKey, this.rest);
        }

        [Test]
        public void Get_Uri()
        {
            string eventId = "EVda9622507c9311e2b21f026ba7cac9da";
            this.service.Event.Get(eventId);
            Assert.AreEqual("https://api.balancedpayments.com/v1/events/EVda9622507c9311e2b21f026ba7cac9da", rest.Uri);            
        }

        [Test]
        public void List_Uri()
        {
            int limit = 5;
            this.service.Event.List(limit);
            Assert.AreEqual("https://api.balancedpayments.com/v1/events?limit=5", rest.Uri);    
        }
    }
}
