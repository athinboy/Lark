using Feign.Core.Attributes;
using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Feign.Core.Context
{
    internal class ParameterWrapContext : ContextBase
    {
        public List<HeaderAttribute> MyHeaderAttributes { get; set; } = new List<HeaderAttribute>();
        public List<QueryStringAttribute> MyQueryStringAttributes { get; set; } = new List<QueryStringAttribute>();

        public MethodWrapContext MethodWrap { get; set; }

        public ParameterInfo Parameter { get; set; }

        public bool JsonSerialize { get; set; } = true;

        public bool XmlSerialize { get; set; } = false;

        public bool IsBody { get; set; } = false;

        public bool IsQueryStr { get; set; } = true;

        public string Name { get; set; } = string.Empty;
        public string QueryString { get; internal set; } = string.Empty;

        private ParameterWrapContext()
        {

        }
        public ParameterWrapContext(MethodWrapContext methodWrapContext, ParameterInfo parameter) : this()
        {
            this.Parameter = parameter;
            this.MethodWrap = methodWrapContext;

        }
        internal override void Clear()
        {
            throw new NotImplementedException();
        }

        internal override void AddHeader(RequestCreContext requestCreContext, HttpContent httpContext)
        {
            this.MyHeaderAttributes.ForEach(x=>{
                x.AddParameterHeader(requestCreContext,this,httpContext);
            });

        }

        internal override void AddQueryString(RequestCreContext requestCreContext, HttpContent httpContext)
        {
            this.MyQueryStringAttributes.ForEach(x=>{
                x.AddParameterQueryString(requestCreContext,this,httpContext);
            });
        }


        internal string Serial(object value)
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

            if (this.XmlSerialize)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);
                xmlSerializer.Serialize(stringWriter, value);
                return stringBuilder.ToString();
            }
            if (this.JsonSerialize)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
            return value.ToString();



        }


    }
}
