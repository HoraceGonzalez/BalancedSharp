using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            IBalancedService service = new BalancedService("2d9966f0818611e2bc21026ba7d31e6f");
            var result = service.CurrentMarketplace.CreateAccount();
            Console.WriteLine(result.Error);
            Console.WriteLine(result.StatusCode);

            //IBalancedService service = new BalancedService("2d9966f0818611e2bc21026ba7d31e6f");
            //var result = service.Account.Create("TEST-MP1ofaIAscnChZ3FVJ6KySrZ");
            Console.ReadLine();
        }
    }
}
