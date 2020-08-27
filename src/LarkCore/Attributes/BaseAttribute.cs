using Lark.Core.Context;
using Lark.Core.Context;
using System;

namespace Lark.Core.Attributes
{
    public class BaseAttribute : Attribute
    {


        internal virtual void Validate()
        {

        }

        internal virtual void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

        }

        internal virtual void SaveToReturnContext(ReturnContext returnContext)
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
