using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspNet.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase, IStudentController
    {
        public string SayHello()
        {
            return "Hello!";
        }
    }
}
