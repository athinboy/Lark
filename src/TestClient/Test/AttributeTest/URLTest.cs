using Feign.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;
using System;
using Feign.Core;

namespace TestClient.Test.AttributeTest
{
    public class URLAttributeTest : TestBase
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
            student.SayHello();
            WrapBase wrap = (WrapBase)student;
            Assert.IsTrue(wrap.Url == BaseUrl);
            Assert.IsTrue(wrap.GetRequestCreURL() == BaseUrl + @"/api/student/sayhello");


        }

    }
}