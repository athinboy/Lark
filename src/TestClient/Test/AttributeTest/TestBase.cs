using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using Feign.Core.Attributes;
using Feign.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;

/// <summary>
///  Test Base Class;
/// </summary>
namespace TestClient.Test.AttributeTest
{
    public class TestBase
    {

        protected const string BaseUrl = "http://localhost:6346";

        [SetUp]
        public void BaseSetup()
        {
            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;      
        }
    }
}