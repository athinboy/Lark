using Feign.Core.Attributes.RequestService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// xml serialize.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class XmlAttribute : Attribute, IValueSerializeService
    {
        string IValueSerializeService.Serial(MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object value)
        {

            if (value == null)
            {
                return string.Empty;
            }
            Type type=value.GetType();
            if (type.IsValueType)
            {
                return value.ToString();
            }
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, value);
            return stringBuilder.ToString();


        }
    }
}
