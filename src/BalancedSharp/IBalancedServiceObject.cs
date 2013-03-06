using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public interface IBalancedServiceObject
    {
        IBalancedService Service { get; set; }
    }
}
