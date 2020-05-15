using Feign.Core.Attributes.RequestService;
using Feign.Core.Cache;
using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
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


    }

}
