using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using Lark.Core.Attributes;
using Lark.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;

/// <summary>
/// The Whole  Attribute Related Test。
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
            Lark.Core.InternalConfig.EmitTestCode = true;
            Lark.Core.InternalConfig.SaveResponse = true;
            Lark.Core.InternalConfig.LogRequest = true; 
 
        }



        //[Test]
        public void WrapTest_One()
        {
            IStudentService student = Lark.Core.Lark.Wrap<IStudentService>("");

            for (int i = 0; i < 1; i++)
            {
                student = Lark.Core.Lark.Wrap<IStudentService>(BaseUrl);
                WrapBase wrap = (WrapBase)student;
                // Assert.IsTrue("Hello!" == student.SayHello("myid","testheader",null));
                HttpResponseMessage responseMessage = wrap.OriginalResponseMessage;
                ProbeInfo probeInfo = Util.GetProbe(responseMessage) ?? throw new Exception("probeinfo is null");
                Assert.IsTrue(probeInfo.Url.EndsWith("/api/student/sayhello"));
                Assert.IsTrue(probeInfo.Method == "GET");
                Assert.IsTrue(probeInfo.Headers.Exists(x => x.Key == "myheader" && x.Value == "hello"));


            }

        }

    }
}