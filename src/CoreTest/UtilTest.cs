using NUnit.Framework;
using System.Collections.Generic;

namespace CoreTest
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        public void GetPathParaName()
        {
            IEnumerable<string> names = Lark.Core.Util.GetPathParaName(@"\aaa\{\\234234\fwef}\{name}\{id}\{name}\2342{\{\}|{}\{id234}");
            System.Console.WriteLine(string.Join(",", names));
            Assert.IsTrue("name,id".Equals(string.Join(",", names)));
        }

    }
}


