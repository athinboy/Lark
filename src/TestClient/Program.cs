using Feign.Core.Exception;
using System;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            FeignException feignException = new Feign.Core.Exception.FeignException("{0}-{1}", "0", "1");
            //Console.WriteLine(feignException.Message);
            Console.WriteLine(Feign.Core.Util.NormalizeURL("fwef/f23f2f///"));
            Console.WriteLine(Feign.Core.Util.NormalizeURL(null));
            Console.WriteLine(Feign.Core.Util.NormalizeURL(""));
        }
    }
}
