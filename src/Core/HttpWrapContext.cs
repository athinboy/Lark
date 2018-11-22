using Feign.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core
{
    internal class HttpWrapContext
    {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }

        public List<HeaderAttribute> headerAttributes = new List<HeaderAttribute>();
        





    }
}
