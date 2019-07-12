using Feign.Core.Attributes.RequestService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// Header attribute.
    /// 
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Parameter,
        Inherited = true, AllowMultiple = true)]
    public sealed class HeaderAttribute : Attribute, IRequestConstructService
    {
        //todo 添加值实现

        public String Name { get; set; } = "";
        public string Value { get; set; } = "";

        /// <summary>
        /// for mehtod or interface.  <code>public HeaderAttribute(string name) </code> is for parameter. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public HeaderAttribute(string name, string value)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name.Trim()) ??
                throw new ArgumentNullException(nameof(name));
            Value = ((value ?? "").Trim().Length == 0 ? null : value.Trim()) ?? "";
        }
        /// <summary>
        /// for parameter .
        /// </summary>
        /// <param name="name"></param>
        public HeaderAttribute(string name)
        {

            Name = (name.Trim().Length == 0 ? null : name) ??
            throw new ArgumentNullException(nameof(name));

        }

        void IRequestConstructService.Construct(HttpClient httpClient, HttpContent httpContent, MethodWrapContext methodWrapContext)
        {
            httpContent.Headers.Add(this.Name, this.Value);

        }

        void IRequestConstructService.Construct(HttpClient httpClient, HttpContent httpContent, MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object para)
        {
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
            httpContent.Headers.Add(this.Name, serialStr);

        }
    }

}