
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

        [HttpGet("sayhello")]
        public string SayHello()
        {
            return "Hello!";
        }

        //http://localhost:5000/api/student/add?Name=56156156156156
        [HttpGet("add")]
        public string Add([FromQuery] Student s)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }

        [HttpGet("add2")]
        public string Add2([FromQuery] Student s, [FromQuery] StudentClass studentClass, [FromQuery] Remark remark)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(s)
            + Newtonsoft.Json.JsonConvert.SerializeObject(studentClass)
            + Newtonsoft.Json.JsonConvert.SerializeObject(remark);
        }

        [HttpGet("addlist")]
        public OkObjectResult AddList([FromQuery] List<Student> ss, [FromQuery] List<StudentClass> studentClasses)
        {
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(ss)
            + Newtonsoft.Json.JsonConvert.SerializeObject(studentClasses));
        }


        [HttpPost("add")]
        public string AddPost(Student s)
        {
            return s == null ? "null" : Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }
        [HttpPost("addform")]
        public string AddPostForm([FromForm]Student s)
        {
            return s == null ? "null" : Newtonsoft.Json.JsonConvert.SerializeObject(s);
        }


        [HttpPost("addlist")]
        public OkObjectResult AddPostList(List<Student> ss)
        {
            return Ok(ss);
        }
        [HttpPost("addlist2")]
        public OkObjectResult AddPostList2(List<Student> ss, [FromQuery] List<StudentClass> studentClasses)
        {
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(ss)
            + Newtonsoft.Json.JsonConvert.SerializeObject(studentClasses));
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
        public Student DelById([FromQuery] int id, [FromQuery(Name = "name")] string theName, int age)
        {
            return new Student() { ID = id, Name = theName, Age = age };
        }


    }
}
