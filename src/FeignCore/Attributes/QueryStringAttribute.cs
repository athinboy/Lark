using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Context;

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



        internal void AddParameterQueryString(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, HttpContent httpContent)
        {
            object value = requestCreContext.ParaValues[parameterWrap.Parameter.Position];

            string valueStr = parameterWrap.Serial(value);
            parameterWrap.QueryString = this.Name + "=" + valueStr;
            

        }






    }
}
