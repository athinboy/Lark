using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient.Test
{
    public class WrapTest
    {

 
        public interface IStudent
        {
 
            void A();
 
            string B(string name);
 
            void C(string name);
        }



        [NUnit.Framework.Test]
        public void One() {


            IStudent student = Feign.Core.Feign.Wrap<IStudent>("");
            student.A();




        }



    }
}
