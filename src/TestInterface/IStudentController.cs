using Feign.Core.Attributes; 
using Microsoft.AspNetCore.Mvc;


namespace TestInterface
{

    [URL("/api/student")]
    [Header("myheader", "hello")]
 
    public interface IStudentService
    {
        [URL("/sayhello")]    
        [HttpGet("GET")]   
        [Header("myheader", "SayHello")]
        string SayHello();

        

    }




}
