using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Exception
{
    public class RunTimeException : FeignException
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
