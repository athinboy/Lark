using Lark.Core.Attributes.RequestService;
using Lark.Core.Cache;
using Lark.Core.Context;
using Lark.Core.Attributes;
using Lark.Core.Context;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Lark.Core.Attributes
{
    /// <summary>
    ///   name of form or json or xml element .
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public class NameAttribute : BaseAttribute
    {

        public String Name { get; set; } = "";

        public NameAttribute(string name)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name.Trim()) ??
             throw new ArgumentNullException(nameof(name));
        }


        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {
            base.SaveToParameterContext(parameterItem);
            parameterItem.Name = this.Name;
        }
 

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
           
        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
         
        }


    }

}
