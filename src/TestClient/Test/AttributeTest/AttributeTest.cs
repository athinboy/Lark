using System;
using System.Collections.Generic;
using System.Text;
using Feign.Core.Attributes;
using NUnit.Framework;

/// <summary>
/// 整体的Attribute测试。
/// </summary>
namespace TestClient.Test.AttributeTest {

    [TestFixture]
    public class AttributeTest {

        [Header ("myheader", "hello")]
        public interface IStudent {
            [URL ("fwefwe")]
            void A ();

            [URL ("fwefwe")]
            string B (string name);

            [URL ("fwefwe")]
            void C (string name);
        }

        [Test]
        public void WrapTest_One () {
            IStudent student = Feign.Core.Feign.Wrap<IStudent> ("");

            for (int i = 0; i < 1; i++) {
                student = Feign.Core.Feign.Wrap<IStudent> ("");
                student.A ();
                student.C ("CCCCCCCCCC");
                System.Console.WriteLine (student.B ("BBB"));
            }

            student.A ();
            //string b= student.B("BBB");
            //System.Console.WriteLine(b);
            //student.B("BBB");
            System.Console.WriteLine ("ffff");
        }

    }
}