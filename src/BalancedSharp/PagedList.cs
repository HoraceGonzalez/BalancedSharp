using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    /// <summary>
    /// Extends a List to provide built in paging.
    /// </summary>
    public class PagedList<T> : List<T>
    {
        public int Limit { get; private set; }
        public int Offset { get; private set; }
        public int Total { get; private set; }
    }
}
