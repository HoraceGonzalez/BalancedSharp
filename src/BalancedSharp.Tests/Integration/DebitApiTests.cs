using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class DebitApiTests
    {
        IBalancedService service;

        public DebitApiTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.ApiKey);
        }

        [Test]
        public void Create_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var debit = account.Result.Debit(
                new Debit()
                {
                    AppearsOnStatementAs = "Statement text",
                    Amount = 500,
                    Description = "Some descriptive text for the debit in the dashboard"
                });
            var item = debit.Result;
        }
    }
}
