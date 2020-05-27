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
    public class PathAttribute : BaseAttribute
    {
        public PathAttribute(string path)
        {
            this.Path = path ?? "";
        }

        public string Path { get; set; }


        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {

            interfaceWrapContext.Path = this.Path;
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
      
            methodWrapContext.Path = this.Path;
        }

        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

        }




    }
}
