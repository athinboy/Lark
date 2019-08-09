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
/// 整体的Attribute测试。
/// </summary>
namespace TestClient.Test.AttributeTest
{



    [TestFixture]
    public class AttributeTest
    {
        private const string BaseUrl = "http://localhost:6346";


        [Test]
        public void WrapTest_One()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("");

            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;


            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudentService>(BaseUrl);
                WrapBase wrap = (WrapBase)student;
                System.Console.WriteLine(student.SayHello());
                HttpResponseMessage responseMessage = wrap.Response;
                IEnumerable<string> ies = responseMessage.Headers.GetValues("probeInfo");
                IEnumerator<string> ietors = ies.GetEnumerator();
                string value = null;
                while (ietors.MoveNext())
                {
                    value = ietors.Current;
                }
             
                if (string.IsNullOrEmpty(value) == false)
                {


                    value = WebUtility.HtmlDecode(value);
                    System.Console.WriteLine(value);

                    ProbeInfo probeInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProbeInfo>(value);

                }





            }

        }

    }
}