using System;
using System.Collections.Generic;
using System.Text;

namespace Lark.Core.Exception
{
    public class RunTimeException : LarkException
    {


        public RunTimeException(string message) : base(message)
        {
        }
        public RunTimeException(string message, params string[] args) : base(message)
        {
            this.args = args;
        }


    }
}
