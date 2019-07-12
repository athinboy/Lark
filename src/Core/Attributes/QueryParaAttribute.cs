using Feign.Core.Attributes.RequestService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// query para.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter,
        Inherited = true, AllowMultiple = false)]
    public class QueryParaAttribute : Attribute, IUrlService
    {

        public String Name { get; set; } = "";

        public QueryParaAttribute(string name)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name.Trim()) ??
             throw new ArgumentNullException(nameof(name));
        }

        string IUrlService.Construct(HttpClient httpClient, HttpContent httpContent, MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object para)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<Attribute> attributes = parameterInfo.GetCustomAttributes();
            IEnumerator<Attribute> enumerator = attributes.GetEnumerator();

            string serialStr = null;

            while (enumerator.MoveNext())
            {
                Attribute attribute = enumerator.Current;
                if (attribute.GetType().IsAssignableFrom(typeof(XmlAttribute)))
                {
                    IValueSerializeService valueSerializeService = attribute as IValueSerializeService;
                    serialStr = valueSerializeService.Serial(methodWrapContext, parameterInfo, para);
                }
                if (attribute.GetType().IsAssignableFrom(typeof(JsonAttribute)))
                {
                    IValueSerializeService valueSerializeService = attribute as IValueSerializeService;
                    serialStr = valueSerializeService.Serial(methodWrapContext, parameterInfo, para);
                }
            }
            if (serialStr == null)
            {
                serialStr = para.ToString();
            }
            stringBuilder.Append(this.Name).Append("=").Append(serialStr);
            return stringBuilder.ToString();
        }

    }

}
