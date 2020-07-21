using Feign.Core;
using Feign.Core.ProxyFactory;
using Newtonsoft.Json;
using NUnit.Framework;
using TestInterface;

namespace TestClient.Test.AttributeTest
{
    public class BodyTest : TestBase
    {
        [SetUp]
        public void BaseSetup1()
        {
            Feign.Core.InternalConfig.NotRequest = false;
            Feign.Core.InternalConfig.SaveRequest = true;
            Feign.Core.InternalConfig.EmitTestCode = true;
            Feign.Core.InternalConfig.SaveResponse = true;
            Feign.Core.InternalConfig.LogRequest = true;
        }

        [NUnit.Framework.Test]
        public void Test()
        {

            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");

            IStudentService.Student s1 = new IStudentService.Student() { Name = "studnetName" };

            string resultstr;

            resultstr = student.AddPost(s1);
            System.Console.WriteLine(resultstr);
            IStudentService.Student s2 = JsonConvert.DeserializeObject<IStudentService.Student>(resultstr);
            Assert.IsTrue(s2 != null && s2.Name == s1.Name);


            resultstr = student.AddPost2(s1.Name);
            System.Console.WriteLine(resultstr);

        }

        [NUnit.Framework.Test]
        public void TestForm()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
            IStudentService.Student s1 = new IStudentService.Student() { Name = "studnetName" };
            string resultstr;
            resultstr = student.AddPostForm(s1);
            System.Console.WriteLine(resultstr);
             IStudentService.Student s2 = JsonConvert.DeserializeObject<IStudentService.Student>(resultstr);
            Assert.IsTrue(s2 != null && s2.Name == s1.Name);

        }





    }
}