
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestInterface;

namespace TestAspNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase, IStudentService
    {
        [HttpGet("sayhello")]
        [HttpPost("sayhello")]
        public string SayHello([FromHeader]string id, [FromHeader]string myheader,[FromHeader] IStudentService.JsonHeader myjsonheader)
        {
            return "Hello!";
        }
        


        [HttpGet]
        public string GetNow()
        {
            return DateTime.Now.ToLongTimeString();
        }

        //http://localhost:5000/api/student/add?Name=56156156156156
        [HttpGet("add")]
        public string Add([FromQuery]Student s)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(s ?? new Student { Name = "name", Age = 234 });
        }

        [HttpPost("add")]
        public string AddPost(Student s)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(s ?? new Student { Name = "name", Age = 234 });
        }

        [HttpGet("getnow2")]
        public string GetNow2()
        {
            return DateTime.Now.ToLongTimeString();
        }

        [HttpGet("getnamestr/{name}")]
        public string GetNameStr(string name)
        {
            return name ?? "null";
        }

        [HttpGet("getnamestr")]
        public string GetNameStr2(string name)
        {
            return name ?? "null";
        }


        [HttpPost("addstr/{name}")]
        public string AddStr(string name)
        {
            return name;
        }

        public string QueryEmpty()
        {
           return "ok";
        }
    }
}
