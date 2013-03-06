using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    public class BankAccountTests
    {
        BalancedService service;

        public void Setup()
        {
            service = new BalancedService(Config.ApiKey);
        }

        public void RetrieveBankAccount_Success()
        {

        }
    }
}
