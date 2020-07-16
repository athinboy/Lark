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
    internal abstract class BodyBind : BindBase
    {

        protected BodyBind()
        {

        }

        private protected List<ParameterWrapContext> parameterWraps = new List<ParameterWrapContext>();
        internal abstract HttpContent Bindbody(RequestCreContext requestCreContext);

        internal void AddPara(ParameterWrapContext parameterWrap)
        {
            this.parameterWraps.Add(parameterWrap);
        }
    }
}