using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// 班长
        /// </summary>
        public Student ClassMonitor { get; set; }
    }
}
