using System.Collections.Generic;
using Lark.Core.Attributes;
using LarkCore.Attributes;


/// <summary>
/// 
/// </summary>
namespace TestInterface
{

    /// <summary>
    /// 此类不应该引用 Microsoft.AspNetCore.Mvc。
    /// </summary> 
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
        [GetMethod]
        // [HttpGet("add")]
        string Add(Student student);



        [Path("add2")]
        [GetMethod]
        string Add2(Student s, [QueryString] StudentClass studentClass, [QueryString("Remark")] Remark remark);


        [PostMethod]
        [Path("add")]
        string AddPost(Student s);

        [PostMethod]
        [Path("add2")]
        string AddPost2(string Name);


        [PostMethod]
        [Path("addform")]
        [FormBody]
        string AddPostForm(Student s);


        [Path("/QueryName")]
        [Header(Name = "appcode", Value = "appcode111111111111", Unique = true)]
        [Header(Name = "supportversion", Value = "3.0", Unique = false)]
        [Header(Name = "case", Value = "lower", Unique = false)]
        string QueryName(int id, [Header(Name = "case", Unique = false)] string stringcase, [Header(Name = "prefix", Unique = true)] string length);


        [Path("/QueryById")]
        Student QueryById(int id);
        [Path("/{id}/{name}/{age}/del")]
        // [HttpGet("/{id}/{name}/{age}/del")]
        Student DelById(int id, [PathPara("name")] string theName, [PathPara] int age);



        [Path("addlist")]
        [PostMethod]
        string AddList([QueryString] List<Student> ss, List<StudentClass> studentClasses);



        [Path("addlist")]
        [PostMethod]
        string AddPostList(List<Student> ss);

        [Path("addlist2")]
        [PostMethod]
        string AddPostList2(List<Student> ss, [QueryString] List<StudentClass> studentClasses);


        [Path("addformlist")]
        [PostMethod]
        [FormBody]
        string AddPostFormList(List<Student> ss);

    }










}
