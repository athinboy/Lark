using NUnit.Framework;
using Feign.Core.Reflect;
using static TestInterface.IStudentService;
using System.Collections.Generic;

namespace CoreTest.ReflectTest
{
    [TestFixture]
    public class TypeReflectorTest
    {
        [Test]
        public void Test()
        {
            int ia = 12;
            TypeCodes typeCode = TypeReflector.GetTypeCodes(ia);
            Assert.IsTrue(typeCode == TypeCodes.int32);


            typeCode = TypeReflector.GetTypeCodes("i");
            Assert.IsTrue(typeCode == TypeCodes.String);


            typeCode = TypeReflector.GetTypeCodes(new Student() { ID = 23 });
            System.Console.WriteLine(typeCode.ToString());
            Assert.IsTrue(typeCode == TypeCodes.complexclass);


        }
        [Test]
        public void Test1()
        {

            Dictionary<string, object> values = DeconstructUtil.Deconstruct(new Student() { ID = 2, Name = "nnnnn", rank = 234 });
            System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(values));


        }





    }
}