using System;
using System.Diagnostics;
using NUnit.Framework;

namespace TestClient.Test.AttributeTest
{
    public class TestTest
    {


        //[NUnit.Framework.Test]
        public void A()
        {
            for (int i = 0; i < 3; i++)
            {
                var watch = Stopwatch.StartNew();
                for (int k = 0; k < 50000; k++)
                {
                    try
                    {
                        var num = int.Parse("xxx");
                    }
                    catch (Exception ex) { }
                }
                watch.Stop();

                Console.WriteLine($"i={i + 1},耗费：{watch.ElapsedMilliseconds}");
            }
            Console.WriteLine("---------------------------------------------");
            for (int i = 0; i < 3; i++)
            {
                var watch = Stopwatch.StartNew();

                for (int k = 0; k < 50000; k++)
                {
                    var num = int.TryParse("xxx", out int reuslt);
                }

                watch.Stop();

                Console.WriteLine($"i={i + 1},耗费：{watch.ElapsedMilliseconds}");
            }
            Console.ReadLine();
        }

    }
}