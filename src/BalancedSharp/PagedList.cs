using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    /// <summary>
    /// Extends a List to provide built in paging.
    /// </summary>
    [DataContract]
    public class PagedList<T>
    {
        [DataMember(Name = "items")]
        public List<T> Items { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; private set; }

        [DataMember(Name = "offset")]
        public int Offset { get; private set; }

        [DataMember(Name = "total")]
        public int Total { get; private set; }
    }
}
