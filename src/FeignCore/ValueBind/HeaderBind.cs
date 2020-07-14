using Feign.Core;
using Feign.Core.Attributes.RequestService;
using Feign.Core.Context;
using Feign.Core.Reflect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FeignCore.ValueBind
{
    public class HeaderBind : BindBase
    {

        /// <summary>
        /// header name;
        /// </summary>
        /// <value></value>
        public String Name { get; set; } = "";

        /// <summary>
        /// the header value .
        /// </summary>
        /// <value></value>
        public string Value { get; set; } = null;

        public FieldInfo Field;

        public PropertyInfo Property;


        /// <summary>
        /// whether the header is Unique.
        /// If it is ,remove exist same name header,then add this header .
        /// false: append this header to header with same name; 
        /// default:true
        /// </summary>
        /// <value></value>
        public bool Unique { get; set; } = DefaultConfig.HeaderUnique;


        public HeaderBind()
        {
            Unique = DefaultConfig.HeaderUnique;
        }


        public HeaderBind(string name) : this()
        {
            Name = name;

        }
        public HeaderBind(string name, bool unique) : this()
        {
            Name = name;
            Unique = unique;
        }



        public HeaderBind(string name, string value, bool unique) : this()
        {
            Name = name;
            Value = value;
            Unique = unique;
        }

        public HeaderBind(string name, FieldInfo field, bool unique) : this()
        {
            Name = name;
            this.Field = field;
            Unique = unique;
        }


        public HeaderBind(string name, PropertyInfo property, bool unique) : this()
        {
            Name = name;
            this.Property = property;
            Unique = unique;
        }

        private string Serial(object o)
        {
            return o == null ? "" : o.ToString();
        }

        private void AddParaValueHeader(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, object paraValue)
        {
            if (paraValue == null)
            {
                AddHeader(requestCreContext, this.Name, "");
                return;
            }

            object pValue;
            string pValueStr = Serial(paraValue);

            if (this.Field != null)
            {
                pValue = this.Field.GetValue(paraValue);
                AddHeader(requestCreContext, this.Name, pValueStr);
                return;

            }
            if (this.Property != null)
            {
                pValue = this.Property.GetValue(paraValue);
                AddHeader(requestCreContext, this.Name, pValueStr);
                return;
            }
            AddHeader(requestCreContext, this.Name, pValueStr);

        }

        internal void AddHeader(RequestCreContext requestCreContext)
        {
            AddHeader(requestCreContext, this.Name, this.Value);

        }

        private void AddHeader(RequestCreContext requestCreContext, string name, string value)
        {
            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            if (this.Unique)
            {                 
                httpContext.Headers.Remove(this.Name);
                httpContext.Headers.Add(this.Name, value);
            }
            else
            {
                httpContext.Headers.Add(this.Name, value);
            }
        }

        internal void AddParameterHeader(RequestCreContext requestCreContext, ParameterWrapContext parameterWrapContext)
        {
            object value = requestCreContext.ParaValues[parameterWrapContext.Parameter.Position];
            AddParaValueHeader(requestCreContext, parameterWrapContext, value);

        }

        public override string ToString()
        {
            return "HeaderBind:" + (this.Name ?? "");
        }
    }
}