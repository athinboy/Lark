using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Feign.Core.Cache;

namespace Feign.Core.Attributes
{
    public class FeignAttribute : Attribute
    {


        internal virtual void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

        }

        internal virtual void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {

        }

        internal virtual void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {

        }



        internal virtual  void AddHeader(RequestCreContext requestCreContext, HttpRequestMessage httpRequestMessage)
        {

        }


        internal  virtual void AddQueryStr(RequestCreContext requestCreContext)
        {

        }



    }
}
