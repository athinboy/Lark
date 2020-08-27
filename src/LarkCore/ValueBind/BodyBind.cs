using Lark.Core;
using Lark.Core.Attributes.RequestService;
using Lark.Core.Context;
using Lark.Core.Reflect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
namespace Lark.Core.ValueBind
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
            parameterWraps.Add(parameterWrap);
        }
    }
}