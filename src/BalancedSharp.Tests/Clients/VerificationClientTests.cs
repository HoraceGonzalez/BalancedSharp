using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class VerificationClientTests
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
        public void Create_Uri()
        {
            this.service.Verification.Create("BA6YSrU2MErCPHAbqecYLCgq");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6YSrU2MErCPHAbqecYLCgq/verifications", this.rest.Uri);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Verification.Get("BA72f0eYCTSDrYurEHls1kNy", "BZ72POZpLd5Z17gSVfPqw9lm");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA72f0eYCTSDrYurEHls1kNy/verifications/BZ72POZpLd5Z17gSVfPqw9lm", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Verification.List("BA7ayh7UWhpV8zUlWJFHiNnW");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA7ayh7UWhpV8zUlWJFHiNnW/verifications", this.rest.Uri);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Verification.List("BA7ayh7UWhpV8zUlWJFHiNnW", limit: 7);
            Assert.AreEqual("7", this.rest.Parameters["limit"]);
        }

        [Test]
        public void Confirm_Uri()
        {
            this.service.Verification.Confirm("BA7fTAfeb4gsHWRSyrhw4ZB5", "BZ7gzdjky0REMD1iFPhFsJh4", 1, 1);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA7fTAfeb4gsHWRSyrhw4ZB5/verifications/BZ7gzdjky0REMD1iFPhFsJh4", this.rest.Uri);
        }

        [Test]
        public void Confirm_Amount1()
        {
            this.service.Verification.Confirm("BA7fTAfeb4gsHWRSyrhw4ZB5", "BZ7gzdjky0REMD1iFPhFsJh4", 25, 1);
            Assert.AreEqual("25", this.rest.Parameters["amount_1"]);
        }
    }
}
