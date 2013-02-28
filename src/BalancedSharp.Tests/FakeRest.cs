using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests
{
    public class FakeRest : IBalancedRest
    {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public Status<T> GetResult<T>(string uri, string username, string password, 
            string method, Dictionary<string, string> parameters)
        {
            this.Uri = uri;
            this.Username = username;
            this.Password = password;
            this.Method = method;
            this.Parameters = parameters;

            return null;
        }


        public Status GetResult(string uri, string username, string password, string method, Dictionary<string, string> parameters)
        {
            this.Uri = uri;
            this.Username = username;
            this.Password = password;
            this.Method = method;
            this.Parameters = parameters;

            return null;
        }
    }
}
