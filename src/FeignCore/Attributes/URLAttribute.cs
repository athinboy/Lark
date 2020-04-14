using System;
using System.Collections.Generic;
using System.Text;
using Feign.Core.Context;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// URL路径。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class,
    Inherited = true, AllowMultiple = false)]
    public class URLAttribute : BaseAttribute
    {
        public URLAttribute(string url)
        {
            this.Url = url ?? "";
        }

        public string Url { get; set; }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            base.SaveToInterfaceContext(interfaceWrapContext);
            interfaceWrapContext.URLAttribute = this;
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            base.SaveToMethodContext(methodWrapContext);
            methodWrapContext.URLAttribute = this;
        }


    }
}
