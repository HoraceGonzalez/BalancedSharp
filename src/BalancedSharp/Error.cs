using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public class Error
    {
        public int StatusCode { get; set; }

        public string Status { get; set; }

        public string Additional { get; set; }

        public string CategoryType { get; set; }

        public string CategoryCode { get; set; }

        public string Description { get; set; }

        public string RequestId { get; set; }
    }
}
