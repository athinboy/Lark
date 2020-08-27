using Lark.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace TestClient.Test.AttributeTest
{
    // [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class HeaderAttributeTest : TestBase
    {
        [SetUp]
        public void BaseSetup1()
        {
            //Lark.Core.InternalConfig.NotRequest = true;
            Lark.Core.InternalConfig.SaveRequest=true;
            
        }
        [NUnit.Framework.Test]
        public void Test1()
        {
            object responseResult;

            IStudentService student = Lark.Core.Lark.Wrap<IStudentService>("http://localhost:6346");
            responseResult= student.QueryName(12, "upper", "234");
            System.Console.WriteLine(responseResult.ToString());
            WrapBase wrap = (WrapBase)student;
            HttpRequestHeaders HttpRequestHeaders = wrap.MyHttpRequestMessagea.Headers;
            HttpContent httpContent = wrap.MyHttpRequestMessagea.Content;

            List<string> values = new List<string>();
            IEnumerable<string> valuesEnumrable = null;

            //appcode:just one
            Assert.IsTrue(HttpRequestHeaders.Contains("appcode") || httpContent.Headers.Contains("appcode"));
            bool l = HttpRequestHeaders.TryGetValues("appcode", out valuesEnumrable) ? true : httpContent.Headers.TryGetValues("appcode", out valuesEnumrable);
            values = valuesEnumrable.ToList();
            values.Sort();
            string valuestr = string.Join(",", values);
            Assert.IsTrue("appcode111111111111" == valuestr);


            //supportversion:3
            Assert.IsTrue(HttpRequestHeaders.Contains("supportversion") || httpContent.Headers.Contains("supportversion"));
            l = HttpRequestHeaders.TryGetValues("supportversion", out valuesEnumrable) ? true : httpContent.Headers.TryGetValues("supportversion", out valuesEnumrable);
            values = valuesEnumrable.ToList();
            Assert.IsTrue(l && values.Count == 3);
            values.Sort();
            valuestr = string.Join(",", values);
            Assert.IsTrue("1.0,2.0,3.0" == valuestr);

            //case:2
            Assert.IsTrue(HttpRequestHeaders.Contains("case") || httpContent.Headers.Contains("case"));
            l = HttpRequestHeaders.TryGetValues("case", out valuesEnumrable) ? true : httpContent.Headers.TryGetValues("case", out valuesEnumrable);
            values = valuesEnumrable.ToList();
            Assert.IsTrue(l && values.Count == 2);
            values.Sort();
            valuestr = string.Join(",", values);
            Assert.IsTrue("lower,upper" == valuestr);


            //prefix:1
            Assert.IsTrue(HttpRequestHeaders.Contains("prefix") || httpContent.Headers.Contains("prefix"));
            l = HttpRequestHeaders.TryGetValues("prefix", out valuesEnumrable) ? true : httpContent.Headers.TryGetValues("prefix", out valuesEnumrable);
            values = valuesEnumrable.ToList();
            Assert.IsTrue(l && values.Count == 1);
            values.Sort();
            valuestr = string.Join(",", values);
            Assert.IsTrue("234" == valuestr);

        }



    }
}