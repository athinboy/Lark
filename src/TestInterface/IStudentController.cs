using Feign.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestInterface
{

    [URL("/api/student")]
    [Header("myheader", "hello")]
    [Method("GET")]
    public interface IStudentService
    {
        [URL("/sayhello")]    
        [Method("GET")]   
        string SayHello();


        //void AddJSON([Json][Name("newstudent")][Body] Student studuent, [Header(name: "creator")] string creator);

        //void AddXML([Xml] Student studuent);


    }




}
