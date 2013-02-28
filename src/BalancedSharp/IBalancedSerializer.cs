using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace BalancedSharp
{
    public interface IBalancedSerializer
    {
        T DeSerialize<T>(string text);

        string Serialize<T>(T item);
    }

    public class DcJsonBalancedSerializer : IBalancedSerializer
    {
        public string Serialize<T>(T item)
        {
            string json;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                json = Encoding.Default.GetString(ms.ToArray());
            }
            return json;
        }

        public T DeSerialize<T>(string text)
        {
            T result = default(T);
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(text)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(ms);
                ms.Close();
            }
            return result;
        }
    }
}
