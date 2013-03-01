using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public class CallbackStatus
    {
        public int failed { get; set; }

        public int pending { get; set; }

        public int retrying { get; set; }

        public int succeeded { get; set; }
    }
}
