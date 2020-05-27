
using Feign.Core;
using Feign.Core.Attributes.RequestService;
using Feign.Core.Context;
using Feign.Core.Reflect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FeignCore.ValueBind
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
            object value = requestCreContext.ParaValues[parameterWrapContext.Parameter.Position];
            string valueStr = parameterWrapContext.Serial(value);
            requestCreContext.FillPath(this.Name, valueStr);
        }
    }
}

