using Feign.Core.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace TestInterface
{

    [Path("/api/student")]
    [Header(Name = "appcode", Value = "appcode12312523523554", Unique = true)]
    [Header(Name = "supportversion", Value = "1.0", Unique = false)]
    [Header(Name = "supportversion", Value = "2.0", Unique = false)]
    [Header(Name = "myschool", Value = "2.0", Unique = false)]
    public interface IStudentService
    {

        public class Student
        {
            public int ID { get; set; }

            public int rank;


            public string Name { get; set; }

            public int Age { get; set; }

            public Student Deskmate { get; set; }

            /// <summary>
            /// 班长
            /// </summary>
            public Student ClassMonitor { get; set; }

        }


        public class StudentClass
        {
            public int number;

            //public string Name { get; set; }

            public string ClassName { get; set; }
        }

        public class Remark
        {
            public string R1;
            public string R2;
        }

        [Path("add")]
        [Method("GET")]
        string Add(Student student);

        [Path("add2")]
        [Method("GET")]
        string Add2(Student s, [QueryString] StudentClass studentClass, [QueryString("Remark")] Remark remark);


        [Method("POST")]
        [Path("add")]
        string AddPost(Student s);

        [Method("POST")]
        [Path("addform")]
        string AddPostForm([FromBody]Student s);


        [Path("/QueryName")]
        [Header(Name = "appcode", Value = "appcode111111111111", Unique = true)]
        [Header(Name = "supportversion", Value = "3.0", Unique = false)]
        [Header(Name = "case", Value = "lower", Unique = false)]
        string QueryName(int id, [Header(Name = "case", Unique = false)] string stringcase, [Header(Name = "prefix", Unique = true)] string length);


        [Path("/QueryById")]
        Student QueryById(int id);

        [Path("/{id}/{name}/{age}/del")]
        Student DelById(int id, [PathPara("name")] string theName, [PathPara] int age);

    }










}
