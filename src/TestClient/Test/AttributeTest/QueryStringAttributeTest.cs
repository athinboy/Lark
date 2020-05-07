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
    public class QueryStringAttribureTest : TestBase
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
            //student.SayHello("1", "2", null);
            //student.QueryEmpty2(new System.Int32());
            // student.QueryEmpty2(45);
            // student.QueryEmpty("myname", 20);
            student.QueryEmpty3("123",2);
            WrapBase wrap = (WrapBase)student;
            HttpRequestHeaders HttpRequestHeaders = wrap.MyHttpRequestMessagea.Headers;
            HttpContent httpContent = wrap.MyHttpRequestMessagea.Content;

        }

    }
}