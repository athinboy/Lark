using Feign.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient.Test.AttributeTest
{
    public class AttributeTest
    {

        [Header("myheader: hello")]
        public interface IStudent
        {
            [URL("fwefwe")]
            void A();

            [URL("fwefwe")]
            string B(string name);
            
            [URL("fwefwe")]
            void C(string name);
        }







        [NUnit.Framework.Test]
        public void WrapTest_One()
        {
            IStudent student = Feign.Core.Feign.Wrap<IStudent>("");

            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudent>("");
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

        public void C<T>(int i, string s, object o,Type type)
        {

            Type t = typeof(T);

            int index = 123;
            System.Console.WriteLine(o.ToString());


            List<object> fwe = new List<object> { i, s, o };


            string dd = (string)o;

            Type otype = o.GetType();


            D(type, index, fwe);
            D(otype, index, fwe);




        }

        public object D(Type t, int i, List<object> d)
        {

            return null;
        }

        public object E(Type t, int i, List<object> d)
        {
            List<object> fwe = new List<object> { };
            return null;
        }

    }
}
