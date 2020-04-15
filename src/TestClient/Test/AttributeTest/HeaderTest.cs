using Feign.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;
using System;
using Feign.Core;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Http;

namespace TestClient.Test.AttributeTest
{
    public class HeaderAttributeTest : TestBase
    {


        [SetUp]
        public void BaseSetup1()
        {
            Feign.Core.InternalConfig.NotRequest = true;
        }


        [NUnit.Framework.Test]
        public void Test()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
            student.SayHello("myid", "testheader", new IStudentService.JsonHeader() { MyName = "myjsonheader" });
            WrapBase wrap = (WrapBase)student;
            HttpRequestHeaders HttpRequestHeaders = wrap.MyHttpRequestMessagea.Headers;
            HttpContent httpContent = wrap.MyHttpRequestMessagea.Content;

            Assert.IsTrue(HttpRequestHeaders.Contains("myheader") || httpContent.Headers.Contains("myheader"));
            Assert.IsTrue(HttpRequestHeaders.Contains("id") || httpContent.Headers.Contains("myheader"));
            Assert.IsTrue(HttpRequestHeaders.Contains("myschool") || httpContent.Headers.Contains("myheader"));
            Assert.IsTrue(HttpRequestHeaders.Contains("myjsonheader") || httpContent.Headers.Contains("myjsonheader"));

            IEnumerable<string> values = new List<string>();
            bool l = HttpRequestHeaders.TryGetValues("myheader", out values) ? true : httpContent.Headers.TryGetValues("myheader", out values);
            foreach (string value in values)
            {
                System.Console.WriteLine("myheader:" + value);
            }
            l = HttpRequestHeaders.TryGetValues("id", out values) ? true : httpContent.Headers.TryGetValues("id", out values);
            foreach (string value in values)
            {
                System.Console.WriteLine("id:" + value);
            }
            l = HttpRequestHeaders.TryGetValues("myschool", out values) ? true : httpContent.Headers.TryGetValues("myschool", out values);
            foreach (string value in values)
            {
                System.Console.WriteLine("myschool:" + value);
            }

            l = HttpRequestHeaders.TryGetValues("myjsonheader", out values) ? true : httpContent.Headers.TryGetValues("myjsonheader", out values);
            foreach (string value in values)
            {
                System.Console.WriteLine("myjsonheader:" + value);
            }

            // myheader:SayHello
            // myheader:"testheader"
            // id:"myid"
            // myschool:school
            // myschool:schoolMethod
            // myjsonheader:{"MyName":"myjsonheader"}



        }

    }
}