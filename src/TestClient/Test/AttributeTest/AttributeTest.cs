using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Feign.Core.Attributes;
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





        [Test]
        public void WrapTest_One()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("");

            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudentService>("http://localhost:6346");
                System.Console.WriteLine(student.SayHello());

            }


            System.Console.WriteLine("ffff");
        }

    }
}