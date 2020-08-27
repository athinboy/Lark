using Lark.Core.Exception;
using System;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            LarkException LarkException = new Lark.Core.Exception.LarkException("{0}-{1}", "0", "1");
            //Console.WriteLine(LarkException.Message);
            Console.WriteLine(Lark.Core.Util.NormalizeURL("fwef/f23f2f///"));
            Console.WriteLine(Lark.Core.Util.NormalizeURL(null));
            Console.WriteLine(Lark.Core.Util.NormalizeURL(""));
        }
    }
}
