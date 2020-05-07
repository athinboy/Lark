using Feign.Core.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace TestInterface
{

    [URL("/api/student")]
    [Header(Name = "myheader", Value = "hellochool", Unique = true)]
    [Header(Name = "myschool", Value = "school", Unique = false)]
    public interface IStudentService
    {

        public class JsonHeader
        {
            public string MyName { get; set; }
        }


        [URL("/sayhello")]
        [HttpGet("GET")]
        [Header("myheader", "SayHello", true)]
        [Header(Name = "myschool", Value = "schoolMethod", Unique = false)]
        string SayHello([Header("id", true)] string id, [Header("myheader", false)] string myheader, [Json][Header("myjsonheader")] JsonHeader myjsonheader);


        [URL("/queryempty")]
        [HttpGet("GET")]
        string QueryEmpty([QueryString("name")] string name, int age);

        [URL("/queryempty")]
        [HttpGet("GET")]
        void QueryEmpty3([QueryString("name")] string name, int age);

        [URL("/queryempty2")]
        [HttpGet("GET")]
        string QueryEmpty2(object ss);



    }




}
