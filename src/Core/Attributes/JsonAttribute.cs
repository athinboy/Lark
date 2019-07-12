using Feign.Core.Attributes.RequestService;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// json serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class JsonAttribute : Attribute, IValueSerializeService
    {
        string IValueSerializeService.Serial(MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            Type type = value.GetType();
            if (type.IsValueType)
            {
                return value.ToString();
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
