using Feign.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient.Test.AttributeTest
{
   public class AttributeTest
    {

        [HeaderAttribute("myheader: hello")]
        interface IStudent
        {

        }
        
        

        [NUnit.Framework.Test]
        public void HeaderAttributeTest_one()
        {


        }

    }
}
