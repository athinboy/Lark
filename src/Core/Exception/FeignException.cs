using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Exception
{
    public class FeignException : System.Exception
    {
        public FeignException(string message) : base(message)
        {
        }
    }
}
