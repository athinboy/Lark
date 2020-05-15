using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Serialize;
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
        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();
        public List<QueryStringAttribute> QueryStringAttributes { get; set; } = new List<QueryStringAttribute>();

        public MethodWrapContext MethodWrap { get; set; }

        public ParameterInfo Parameter { get; set; }

        public SerializeTypes serializeType;

        public bool IsBody { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string QueryString { get; internal set; } = string.Empty;
        
        public PathParaAttribute PathParaAttribute { get; internal set; }


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
            this.PathParaAttribute=null;
            this.HeaderAttributes.Clear();
            this.QueryStringAttributes.Clear();
            this.serializeType = SerializeTypes.json;
            IsBody = false;  
            this.QueryString=string.Empty;
            throw new NotImplementedException();
        }

        internal void FillPath(RequestCreContext requestCreContext)
        {
            if(this.PathParaAttribute!=null){
                PathParaAttribute.FillPath(requestCreContext,this);
            }
        }

        internal override void AddHeader(RequestCreContext requestCreContext)
        {
            this.HeaderAttributes.ForEach(x =>
            {
                x.AddParameterHeader(requestCreContext, this);
            });

        }

        internal override void AddQueryString(RequestCreContext requestCreContext)
        {
            this.QueryStringAttributes.ForEach(x =>
            {
                x.AddParameterQueryString(requestCreContext, this);
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
            if(typeof(string).IsInstanceOfType(value)){
                return value.ToString();
            }
            if(type.IsPrimitive){
                return value.ToString();

            }
            if (this.serializeType == SerializeTypes.tostring)
            {
                return value.ToString();
            }
            if (this.serializeType == SerializeTypes.xml)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);
                xmlSerializer.Serialize(stringWriter, value);
                return stringBuilder.ToString();
            }
            if (this.serializeType == SerializeTypes.json)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
            return value.ToString();



        }


    }
}
