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


        [SetUp]
        public void Setup()
        {
            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;
        }


        [Test]
        public void WrapTest_One()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("");

            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudentService>(BaseUrl);
                WrapBase wrap = (WrapBase)student;
                Assert.IsTrue("Hello!" == student.SayHello());
                HttpResponseMessage responseMessage = wrap.Response;
                ProbeInfo probeInfo = Util.GetProbe(responseMessage) ?? throw new Exception("probeinfo is null");
                Assert.IsTrue(probeInfo.Url.EndsWith("/api/student/sayhello"));
                Assert.IsTrue(probeInfo.Method == "GET");
                Assert.IsTrue(probeInfo.Headers.Exists(x => x.Key == "myheader" && x.Value == "hello"));


            }

        }

    }
}