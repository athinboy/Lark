using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestInterface;

namespace TestClient.Test
{
    class Util
    {



        public static ProbeInfo GetProbe(HttpResponseMessage responseMessage)
        {

            IEnumerable<string> ies = responseMessage.Headers.GetValues("probeInfo");
            IEnumerator<string> ietors = ies.GetEnumerator();
            string value = null;
            while (ietors.MoveNext())
            {
                value = ietors.Current;
            }

            if (string.IsNullOrEmpty(value) == false)
            {


                value = WebUtility.HtmlDecode(value);
                System.Console.WriteLine(value);

                ProbeInfo probeInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProbeInfo>(value);
                return probeInfo;

            }
            return null;



        }










        static void Main(string[] args)
        {

            Run();


        }



        async static void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                await dosomething(i);
            }
        }


        public static async Task<object> dosomething(int i)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " before do1:" + i.ToString());

            Do1(i);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " after do1:" + i.ToString());

            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " before do2:" + i.ToString());
            await Do2(i);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " after do2:" + i.ToString());
            return null;
        }


        public static object Do1(int i)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " do1:" + i.ToString());
            Thread.Sleep(1000);
            return null;
        }

        public static Task<object> Do2(int i)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " do2:" + i.ToString());
            Thread.Sleep(1000);
            return null;

        }


    }
}
