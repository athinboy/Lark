using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Feign.Core.Attributes;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;


/// <summary>
/// 整体的Attribute测试。
/// </summary>
namespace TestClient.Test.AttributeTest
{



    [TestFixture]
    public class AttributeTest
    {

        [Header("myheader", "hello")]
        [Method("GET")]
        public interface IStudentService
        {

            [URL("fwefwe")]
            [HttpGet("fwef")]
            void A();

            [URL("fwefwe")]
            string B(string name);

            [URL("fwefwe")]
            void C(string name);

            void AddJSON([Json][QueryPara("newstudent")][Body] Studuent studuent, [Header(name: "creator")] string creator);

            void AddXML([Xml] Studuent studuent);


        }


        public class Studuent
        {
            public string Name { get; set; }
            public int Age { get; set; }

            /// <summary>
            /// 班长
            /// </summary>
            public Studuent ClassMonitor { get; set; }


        }

        [Test]
        public void WrapTest_One()
        {
            IStudentService student = Feign.Core.Feign.Wrap<IStudentService>("");

            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudentService>("");
                student.A();
                student.C("CCCCCCCCCC");
                System.Console.WriteLine(student.B("BBB"));
            }

            student.A();
            //string b= student.B("BBB");
            //System.Console.WriteLine(b);
            //student.B("BBB");
            System.Console.WriteLine("ffff");
        }

    }
}