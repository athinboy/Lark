using Feign.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient.Test.AttributeTest
{
    public class AttributeTest
    {

        [HeaderAttribute("myheader: hello")]
        public interface IStudent
        {
            void A();

            string B(string name);
            void C(string name);
        }







        [NUnit.Framework.Test]
        public void HeaderAttributeTest_one()
        {
            IStudent student = Feign.Core.Feign.Wrap<IStudent>();

            for (int i = 0; i < 1; i++)
            {
                student = Feign.Core.Feign.Wrap<IStudent>();
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

        public void C<T>(int i, string s, object o)
        {

            Type t = typeof(T);

            int index = 123;
            System.Console.WriteLine(o.ToString());


            List<object> fwe = new List<object> { i, s, o };


            string dd = (string)o;


            D(t, index, fwe);




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
