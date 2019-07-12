using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes.RequestService
{
    internal interface IValueSerializeService
    {
        string Serial(MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object value);
    }
}
