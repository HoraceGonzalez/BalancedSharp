using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests
{
    [TestFixture]
    public class DcJsonBalancedSerializerTests
    {
        DcJsonBalancedSerializer serializer;
        string standardString = "bacon";
        string serializedString = "\"bacon\"";

        [SetUp]
        public void Setup()
        {
            serializer = new DcJsonBalancedSerializer();
        }

        [Test]
        public void Serialize_String_Success()
        {
            string result = serializer.Serialize<string>(standardString);
            Assert.AreEqual(result, serializedString);
        }

        [Test]
        public void DeSerialize_String_Success()
        {
            string result = serializer.DeSerialize<string>(serializedString);
            Assert.AreEqual(result, standardString);
        }
    }
}
