using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public enum BankAccountType
    {
        [EnumMember(Value = "checking")]
        Checking,

        [EnumMember(Value = "savings")]
        Savings
    }
}
