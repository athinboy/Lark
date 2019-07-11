using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient.Test
{
    class Util
    {

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

            await Do1(i);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " after do1:" + i.ToString());

            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " before do2:" + i.ToString());
            await Do2(i);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " after do2:" + i.ToString());
            return null;
        }


        public static async Task<object> Do1(int i)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " do1:" + i.ToString());
            Thread.Sleep(1000);
            return null;
        }

        public static async Task<object> Do2(int i)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss-FFF") + " do2:" + i.ToString());
            Thread.Sleep(1000);
            return null;

        }


    }
}
