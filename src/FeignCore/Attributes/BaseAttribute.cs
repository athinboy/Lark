using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Feign.Core.Cache;

namespace Feign.Core.Attributes
{
    public class BaseAttribute : Attribute
    {


        internal virtual void Validate ()
        {

        }


        internal virtual void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

        }

        internal virtual void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {

        }

        internal virtual void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {

        }


        internal virtual void AddInterfaceQueryString(RequestCreContext requestCreContext, InterfaceWrapContext interfaceWrap, HttpContent httpContext)
        {

        }
        internal virtual void AddMethodQueryString(RequestCreContext requestCreContext, MethodWrapContext methodWrap, HttpContent httpContext)
        {

        }

        internal virtual void AddParameterQueryString(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, HttpContent httpContext,object value)
        {

        }

        virtual internal void AddInterfaceHeader(RequestCreContext requestCreContext, InterfaceWrapContext interfaceWrap, HttpContent httpContext)
        {

        }
        virtual internal void AddMethodHeader(RequestCreContext requestCreContext, MethodWrapContext methodWrap, HttpContent httpContext)
        {

        }

        virtual internal void AddParameterHeader(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, HttpContent httpContext,object value)
        {

        }



    }
}
