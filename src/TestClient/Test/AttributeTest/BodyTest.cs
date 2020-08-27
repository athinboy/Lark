using Lark.Core.ProxyFactory;
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
            Lark.Core.InternalConfig.NotRequest = false;
            Lark.Core.InternalConfig.SaveRequest = true;
            Lark.Core.InternalConfig.EmitTestCode = true;
            Lark.Core.InternalConfig.SaveResponse = true;
            Lark.Core.InternalConfig.LogRequest = true;
        }

        [NUnit.Framework.Test]
        public void Test()
        {

            IStudentService student = Lark.Core.Lark.Wrap<IStudentService>("http://localhost:6346");

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
            IStudentService student = Lark.Core.Lark.Wrap<IStudentService>("http://localhost:6346");
            IStudentService.Student s1 = new IStudentService.Student() { Name = "studnetName" };
            string resultstr;
            resultstr = student.AddPostForm(s1);
            System.Console.WriteLine(resultstr);
             IStudentService.Student s2 = JsonConvert.DeserializeObject<IStudentService.Student>(resultstr);
            Assert.IsTrue(s2 != null && s2.Name == s1.Name);

        }





    }
}