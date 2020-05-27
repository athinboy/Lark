using Feign.Core;
using Feign.Core.ProxyFactory;
using NUnit.Framework;
using TestInterface;

namespace TestClient.Test.AttributeTest
{
    public class BodyTest : TestBase
    {
        [SetUp]
        public void BaseSetup1()
        {
            Feign.Core.InternalConfig.NotRequest = true;
            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;
            Feign.Core.InternalConfig.LogRequest = true;
        }

        //[NUnit.Framework.Test]
        public void Test()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
            student.DelById(111, "nnnnname", 98);
            WrapBase wrap = (WrapBase)student;
            Assert.IsTrue(wrap.Url == BaseUrl);
            System.Console.WriteLine(wrap.GetRequestCreURL());
            Assert.IsTrue(wrap.GetRequestCreURL() == BaseUrl + @"/api/student/111/nnnnname/98/del");

        }



    }
}