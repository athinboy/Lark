using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Exception
{
    public class UnsupportException : FeignException
    {

        public UnsupportException() : base("")
        {
        }


        public UnsupportException(string message) : base(message)
        {
        }
        public UnsupportException(string message, params string[] args) : base(message)
        {
            this.args = args;
        }


    }
}
