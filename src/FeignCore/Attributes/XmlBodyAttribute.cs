using Feign.Core.Attributes.RequestService;
using Feign.Core.Cache;
using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// json serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class XmlBodyAttribute : BaseAttribute
    {
        internal override void SaveToInterfaceContext  (InterfaceWrapContext interfaceWrapContext)
        {
            base.SaveToInterfaceContext(interfaceWrapContext);
            interfaceWrapContext.XmlBody = true;
        }


        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext){
            base.SaveToMethodContext(methodWrapContext);
            methodWrapContext.XmlBody=true;
        }
    }
}
