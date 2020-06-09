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
        public abstract HttpContent Bindbody();
    }
}