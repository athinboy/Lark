using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Context;
using FeignCore.Serialize;

namespace Feign.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class QueryStringAttribute : BaseAttribute
    {
        public string Name { get; set; }

        public QueryStringAttribute()
        {

        }

        public QueryStringAttribute(string name)
        {
            Name = name;
        }


        internal override void Validate()
        {
            Name = ((Name ?? "").Trim().Length == 0 ? null : Name) ??
                throw new ArgumentNullException(nameof(Name));
        }

        internal override void SaveToParameterContext(ParameterWrapContext parameterWrapContext)
        {
            parameterWrapContext.serializeType = SerializeTypes.tostring;
        }

        internal void AddParameterQueryString(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap)
        {
            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            object value = requestCreContext.ParaValues[parameterWrap.Parameter.Position];
            string valueStr = parameterWrap.Serial(value);
            parameterWrap.QueryString = this.Name + "=" + valueStr;
            requestCreContext.QueryString.Add(this.Name,valueStr);

        }






    }
}
