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
                "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ",
                Config.ApiKey, this.rest);
        }

        [Test]
        public void Get_Uri()
        {
            string eventId = "";
            this.service.Event.Get(eventId);
            //Add More
        }
    }
}
