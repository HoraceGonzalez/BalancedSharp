using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public static class EnumDeserializationHelper
    {
        public static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumeration.");
            return (T)Enum.Parse(typeof(T), value.First().ToString().ToUpper() + value.Substring(1));
        }
    }
}
