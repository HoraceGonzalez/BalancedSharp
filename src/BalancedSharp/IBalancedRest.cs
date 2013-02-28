using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace BalancedSharp
{
    public interface IBalancedRest
    {
        Status<T> GetResult<T>(string uri, string username, string password, 
            string method, Dictionary<string, string> parameters);
    }

    public class HttpWebRequestRest : IBalancedRest
    {
        IBalancedSerializer serializer;

        public HttpWebRequestRest(IBalancedSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string BuildParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null) 
                return String.Empty;
            if (parameters.Count < 1)
                return String.Empty;
            
            StringBuilder final = new StringBuilder();
            foreach (var item in parameters)
            {
                final.AppendFormat("{0}={1}&", 
                    Uri.EscapeDataString(item.Key), 
                    Uri.EscapeDataString(item.Value));
            }
            final.Remove(final.Length - 1, 1);
            return final.ToString();
        }

        public Status<T> GetResult<T>(string uri, string username, string password, 
            string method, Dictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri", "uri is required");
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method", "method is required");
            
            string data = BuildParameters(parameters);

            // create the request
            HttpWebRequest request;
            if (method.ToLower() == "get" && !string.IsNullOrEmpty(data))
                request = (HttpWebRequest)WebRequest.Create(uri = "?" + data);
            else
                request = (HttpWebRequest)WebRequest.Create(uri);

            // setup the authorization header
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                if(username == null) 
                    username = String.Empty;
                if(password == null) 
                    password = String.Empty;
                request.Headers[HttpRequestHeader.Authorization] =
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("Basic {0}:{1}", username, password)));
            }

            // setup the method
            switch (method.ToLower())
            {
                default:
                    request.Method = "GET";
                    break;
            }
            

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

            }


            return null;
        }
    }
}
