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
            this.service = new BalancedService(Config.ApiKey, this.rest);
        }

        [Test]
        public void CreateNewBank_Params()
        {
            string creditsUri = "https://api.balancedpayments.com/v1/credits";
            int amount = 1000;
            string name = "Tony Stark";
            string accountNumber = "9900000001";
            string routingNumber = "121000358";
            string type = "checking";

            this.service.Credit.CreateNewBank(
                creditsUri,
                amount,
                name,
                accountNumber,
                routingNumber,
                type
            );
            Assert.AreEqual(amount.ToString(), this.rest.Parameters["amount"]);
            Assert.AreEqual(name, this.rest.Parameters["bank_account[name]"]);
            Assert.AreEqual(accountNumber, this.rest.Parameters["bank_account[account_number]"]);
            Assert.AreEqual(routingNumber, this.rest.Parameters["bank_account[routing_number]"]);
            Assert.AreEqual(type, this.rest.Parameters["bank_account[type]"]);
        }

        [Test]
        public void CreateBank_Params()
        {
            string creditsUri = "https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs/credits";
            int amount = 1000;
            
            this.service.Credit.CreateBank(creditsUri, amount);
            Assert.AreEqual(amount.ToString(), this.rest.Parameters["amount"]);
        }

        [Test]
        public void CreateAccount_Params()
        {
            string creditsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/credits";
            int amount = 1000;

            this.service.Credit.CreateAccount(creditsUri, amount);
            Assert.AreEqual(amount.ToString(), this.rest.Parameters["amount"]);
        }

        [Test]
        public void List_Params()
        {
            string creditsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/credits";
            int limit = 10;
            int offset = 0;
            
            this.service.Credit.List(
                creditsUri,
                limit: limit,
                offset: offset
            );
            Assert.AreEqual(limit.ToString(), this.rest.Parameters["limit"]);
            Assert.AreEqual(offset.ToString(), this.rest.Parameters["offset"]);
        }
    }
}
