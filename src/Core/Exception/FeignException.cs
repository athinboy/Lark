using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Exception
{
    public class FeignException : System.Exception
    {

        protected string[] args;

        public FeignException(string message) : base(message)
        {
        }
        public FeignException(string message, params string[] args) : base(message)
        {
            this.args = args;
        }

        public override string Message
        {

            get
            {
                if (args == null || args.Length == 0)
                {
                    return base.Message;
                }
                return string.Format(base.Message, args);
            }
        }
    }
}
