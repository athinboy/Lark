using Feign.Core.Attributes.RequestService;
using Feign.Core.Context;
using Feign.Core.Reflect;
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
    public sealed class HeaderAttribute : BaseAttribute
    {


        public HeaderAttribute()
        {
        }

        public HeaderAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public HeaderAttribute(string name, string value, bool unique)
        {
            this.Name = name;
            this.Value = value;
            this.Unique = unique;
        }
        public HeaderAttribute(string name)
        {
            this.Name = name;
        }


        public HeaderAttribute(string name, bool unique)
        {
            this.Name = name;
            this.Unique = unique;
        }


        public String Name { get; set; } = "";
        /// <summary>
        /// the header value .ignore for parameter.
        /// </summary>
        /// <value></value>
        public string Value { get; set; } = "";

        /// <summary>
        /// whether the header is Unique.
        /// If it is ,remove exist same name header,then add this header .
        /// false: append this header to header with same name; 
        /// default:true
        /// </summary>
        /// <value></value>
        public bool Unique { get; set; } = true;

        internal override void Validate()
        {
            Name = ((this.Name ?? "").Trim().Length == 0 ? null : Name.Trim()) ??
                throw new ArgumentNullException(nameof(Name));
            Value = ((Value ?? "").Trim().Length == 0 ? string.Empty : Value.Trim()) ?? "";

        }

        internal void AddInterfaceHeader(RequestCreContext requestCreContext, InterfaceWrapContext interfaceWrap)
        {
            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            if (this.Unique)
            {
                httpContext.Headers.Remove(this.Name);
                httpContext.Headers.Add(this.Name, this.Value);
            }
            else
            {
                httpContext.Headers.Add(this.Name, this.Value);
            }
        }
        internal void AddMethodHeader(RequestCreContext requestCreContext, MethodWrapContext methodWrap)
        {
            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            if (this.Unique)
            {
                httpContext.Headers.Remove(this.Name);
                httpContext.Headers.Add(this.Name, this.Value);
            }
            else
            {
                httpContext.Headers.Add(this.Name, this.Value);
            }
        }

        internal void AddParameterHeader(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap)
        {

            if(TypeReflector.IsComplextClass(parameterWrap.Parameter.ParameterType)){
                throw new NotSupportedException("暂时不支持复杂类型用于header、pathpara");
            }

            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            object value = requestCreContext.ParaValues[parameterWrap.Parameter.Position];
            string valueStr = parameterWrap.Serial(value);
            if (this.Unique)
            {
                httpContext.Headers.Remove(this.Name);
                httpContext.Headers.Add(this.Name, valueStr);
            }
            else
            {
                httpContext.Headers.Add(this.Name, valueStr);
            }
        }


    }






}