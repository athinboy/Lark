
using Feign.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestInterface;
using static TestInterface.IStudentService;

namespace TestAspNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentController : ControllerBase, IStudentService
    {
        [HttpGet]
        public string GetNow()
        {
            return DateTime.Now.ToLongTimeString();
        }

        //http://localhost:5000/api/student/add?Name=56156156156156
        [HttpGet("add")]
        public string Add([FromQuery] Student s)
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


        public string QueryEmpty2(object ss)
        {
            throw new NotImplementedException();
        }

        public string QueryName(int id, [Header(Name = "case", Value = "low", Unique = false)] string stringcase, [Header(Name = "prefix", Value = "2", Unique = true)] string length)
        {
            throw new NotImplementedException();
        }

        public Student QueryById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/{id}/{name}/{age}/del")]
        public Student DelById([FromQuery] int id, [FromQuery(Name = "name")] string theName,int age)
        {
            return new Student() { ID = id, Name = theName,Age=age };
        }
    }
}
