using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using Feign.Core.Context;

namespace Feign.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class QueryStringAttribute : BaseAttribute
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public QueryStringAttribute(string name, string value)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name) ??
                throw new ArgumentNullException(nameof(name));
            Value = (value.Trim().Length == 0 ? null : value) ?? "";
        }

        public string GetQueryString()
        {
            return this.Name + "=" + this.Value;
        }





        internal override void AddInterfaceQueryString(RequestCreContext requestCreContext, InterfaceWrapContext interfaceWrap, HttpContent httpContext)
        {

        }
        internal override void AddMethodQueryString(RequestCreContext requestCreContext, MethodWrapContext methodWrap, HttpContent httpContext)
        {

        }

        internal override void AddParameterQueryString(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, HttpContent httpContext, object value)
        {

        }





    }
}
