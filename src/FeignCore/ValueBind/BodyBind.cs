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
    public abstract class BodyBind : BindBase
    {
        private protected List<ParameterWrapContext> parameterWraps = new List<ParameterWrapContext>();
        public abstract HttpContent Bindbody();

        internal void AddPara(ParameterWrapContext parameterWrap)
        {
            this.parameterWraps.Add(parameterWrap);
        }
    }
}