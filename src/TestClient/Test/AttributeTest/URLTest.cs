using Feign.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;
using System;
using Feign.Core;
using System.Net.Http.Headers;
using System.Net.Http;

namespace TestClient.Test.AttributeTest
{
    public class URLAttributeTest : TestBase
    {


        [SetUp]
        public void BaseSetup1()
        {
            Feign.Core.InternalConfig.NotRequest = true;
            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;
            Feign.Core.InternalConfig.LogRequest = true;
            Feign.Core.InternalConfig.LogResponse = true;
            Feign.Core.InternalConfig.SaveRequest=true;
        }


        [NUnit.Framework.Test]
        public void Test()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
            student.DelById(111, "nnnnname", 98);
            WrapBase wrap = (WrapBase)student;
            Assert.IsTrue(wrap.Url == BaseUrl);
            System.Console.WriteLine(wrap.GetRequestCreURL());
            Assert.IsTrue(wrap.GetRequestCreURL() == BaseUrl + @"/api/student/111/nnnnname/98/del");

        }

        [NUnit.Framework.Test]
        public void Test2()
        {

            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
            student.Add(new IStudentService.Student() { ID = 1, Name = "name", rank = 23 });

            WrapBase wrap = (WrapBase)student;
            HttpRequestHeaders httpRequestHeaders = wrap.MyHttpRequestMessagea.Headers;
            HttpContent httpContent = wrap.MyHttpRequestMessagea.Content;
            System.Console.WriteLine(wrap.GetRequestCreURL());
            //Assert.IsTrue(wrap.GetRequestCreURL() == BaseUrl + @"/api/student/add?ID=1&Name=name&rank=23");

            //--------------------------------------
            student.Add2(new IStudentService.Student() { ID = 1, Name = "name", rank = 23 }, new IStudentService.StudentClass() { }, new IStudentService.Remark() { });
            wrap = (WrapBase)student;
            httpRequestHeaders = wrap.MyHttpRequestMessagea.Headers;
            httpContent = wrap.MyHttpRequestMessagea.Content;
            System.Console.WriteLine(wrap.GetRequestCreURL());

            //Assert.Catch( delegate  {} ,"",null);

        }

    }
}