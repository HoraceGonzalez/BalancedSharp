using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BalancedSharp
{
    public interface IBalancedRest
    {
        Status<T> GetResult<T>(string uri, string username, string password, 
            string method, Dictionary<string, string> parameters);

        Status GetResult(string uri, string username, string password,
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
                if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value))
                {
                    final.AppendFormat("{0}={1}&",
                        Uri.EscapeDataString(item.Key),
                        Uri.EscapeDataString(item.Value));
                }
            }
            final.Remove(final.Length - 1, 1);
            return final.ToString();
        }

        public int GetResultContent(string uri, string username, string password,
            string method, Dictionary<string, string> parameters, out string content)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri", "uri is required");
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method", "method is required");

            string data = BuildParameters(parameters);

            // create the request
            HttpWebRequest request;
            if (method.ToLower() == "get" && !string.IsNullOrEmpty(data))
                request = (HttpWebRequest)WebRequest.Create(uri = uri + "?" + data);
            else
                request = (HttpWebRequest)WebRequest.Create(uri);

            // setup the authorization header
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                if (username == null)
                    username = String.Empty;
                if (password == null)
                    password = String.Empty;
                request.Credentials = new NetworkCredential(username, password);
            }

            // setup the method
            switch (method.ToLower())
            {
                case "put":
                    request.Method = "PUT";
                    break;
                case "delete":
                    request.Method = "DELETE";
                    break;
                case "post":
                    request.Method = "POST";
                    break;
                default:
                    request.Method = "GET";
                    break;
            }

            // send the data
            if (request.Method == "PUT" || request.Method == "POST")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
            }

            // attempt to get the results
            HttpWebResponse response;
            content = "";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // handle 400 and 500 level errors
                response = ((HttpWebResponse)ex.Response);
            }

            int statusCode = (int)response.StatusCode;

            // read the content from the server
            using (Stream dataStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(dataStream))
            {
                content = reader.ReadToEnd();
            }

            return statusCode;
        }

        public Status GetResult(string uri, string username, string password,
            string method, Dictionary<string, string> parameters)
        {
            string content;
            int statusCode = GetResultContent(uri, username, password, method, parameters, out content);
            if (statusCode < 400)
                return new Status()
                {
                    StatusCode = statusCode
                };
            else
                return new Status()
                {
                    StatusCode = statusCode,
                    Error = serializer.DeSerialize<Error>(content)
                };
        }

        public Status<T> GetResult<T>(string uri, string username, string password, 
            string method, Dictionary<string, string> parameters)
        {
            string content;
            int statusCode = GetResultContent(uri, username, password, method, parameters, out content);

            if (statusCode < 400)
                return Status.OK(serializer.DeSerialize<T>(content));
            else
                return Status.Failed<T>(statusCode, serializer.DeSerialize<Error>(content), default(T));
        }
    }
}
