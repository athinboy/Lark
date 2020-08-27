using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Feign.Core.Context;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// http method 
    /// default: Post;
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface,
        Inherited = true, AllowMultiple = false)]
    public abstract class MethodAttribute : BaseAttribute
    {

        protected string Method { get; set; } = HttpMethod.Get.Method;

        public MethodAttribute(string httpmethod)
        {
            this.Method = httpmethod.ToUpper();
        }

        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            methodWrapContext.HttpMethod = this.Method.ToUpper();
        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.HttpMethod = this.Method.ToUpper();
        }




    }
}