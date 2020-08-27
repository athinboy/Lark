
using Lark.Core;
using Lark.Core.Attributes.RequestService;
using Lark.Core.Context;
using Lark.Core.Reflect;
using Lark.Core.ValueBind;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Lark.Core.ValueBind
{
    public class PathParaBind : BindBase
    {

        public string Name;


        public PathParaBind(string name)
        {
            Name = name.ToLower();
        }


        internal void FillPath(RequestCreContext requestCreContext, ParameterWrapContext parameterWrapContext)
        {
            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            object value = requestCreContext.ParameterValues.Value[parameterWrapContext.Parameter.Position];
            string valueStr = parameterWrapContext.Serial(value);
            requestCreContext.FillPath(this.Name, valueStr);
        }
    }
}

