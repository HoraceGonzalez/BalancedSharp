using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class BankAccountClientTests
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
        public void Create_Params()
        {
            string bankAccountUri = "https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs";
            string name = "Johann Bernoulli";
            string accountNumber = "9900000001";
            string routingNumber = "121000358";
            BankAccountType type = BankAccountType.Checking;

            this.service.BankAccount.Create(bankAccountUri, name, accountNumber, routingNumber, type);
            Assert.AreEqual(name, this.rest.Parameters["name"]);
            Assert.AreEqual(accountNumber, this.rest.Parameters["account_number"]);
            Assert.AreEqual(routingNumber, this.rest.Parameters["routing_number"]);
            Assert.AreEqual(type, this.rest.Parameters["type"]);
        }

        [Test]
        public void List_Params()
        {
            string bankAccountUri = "https://api.balancedpayments.com/v1/bank_accounts";
            int limit = 10;
            int offset = 0;
            this.service.BankAccount.List(bankAccountUri, limit: limit, offset: offset);
            Assert.AreEqual(limit.ToString(), this.rest.Parameters["limit"]);
            Assert.AreEqual(offset.ToString(), this.rest.Parameters["offset"]);
        }
    }
}
