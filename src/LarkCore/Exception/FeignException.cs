using System;
using System.Collections.Generic;
using System.Text;

namespace Lark.Core.Exception
{
    public class LarkException : System.Exception
    {

        protected string[] args;

        public LarkException(string message) : base(message)
        {
        }
        public LarkException(string message, params string[] args) : base(message)
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
