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
 



    }
}
