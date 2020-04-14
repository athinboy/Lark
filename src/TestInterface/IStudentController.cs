using Feign.Core.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace TestInterface
{

    [URL("/api/student")]
    [Header(Name = "myheader", Value = "hellochool", Unique = true)]
    [Header(Name = "myschool", Value = "school", Unique = false)]
    public interface IStudentService
    {
        [URL("/sayhello")]
        [HttpGet("GET")]
        [Header("myheader", "SayHello", true)]
        [Header(Name = "myschool", Value = "schoolMethod", Unique = false)]
        string SayHello([Header("id", true)] string id, [Header("myheader", false)] string myheader);

        [URL("/sayhello")]    
        [HttpGet("GET")] 
        string TestURL();



    }




}
