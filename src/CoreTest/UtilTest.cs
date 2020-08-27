using NUnit.Framework;
using Feign.Core.Reflect;
using static TestInterface.IStudentService;
using System.Collections.Generic;
using Feign.Core;

namespace CoreTest
{
        [TestFixture]
    public class UtilTest
    {
        [Test]
        public void GetPathParaName(){
        IEnumerable<string> names=   Util.GetPathParaName(@"\aaa\{\\234234\fwef}\{name}\{id}\{name}\2342{\{\}|{}\{id234}");
        System.Console.WriteLine(string.Join(",",names)); 
        Assert.IsTrue("name,id".Equals(string.Join(",",names)));
        }

    }
}


