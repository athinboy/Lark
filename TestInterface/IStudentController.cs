using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestAspNet.Controllers
{
   

    public interface IStudentController
    {
        [HttpGetAttribute]      
        string SayHello();
    }
}
