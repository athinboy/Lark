using Lark.Core.Context;
using Lark.Core.ValueBind;
using System;
using System.Net.Http;
using System.Reflection;

namespace Lark.Core.ValueBind
{
    public class HeaderBind : BindBase, ICloneable
    {

        public enum Source
        {
            FromInterface,
            FromMethod,
            FromParameter
        }

        private Source source;


        public bool FromInterface { get { return this.source == Source.FromInterface; } private set { } }
        public bool FromMethod { get { return this.source == Source.FromMethod; } private set { } }
        public bool FromParameter { get { return this.source == Source.FromParameter; } private set { } }
        public bool Enable { get; set; } = true;

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





        /// <summary>
        /// whether the header is Unique.
        /// If it is ,remove exist same name header,then add this header .
        /// false: append this header to header with same name; 
        /// default:true
        /// </summary>
        /// <value></value>
        public bool Unique { get; set; } = DefaultConfig.HeaderUnique;


        public HeaderBind(Source source)
        {
            Unique = DefaultConfig.HeaderUnique;
            this.source = source;
        }


        public HeaderBind(Source source, string name) : this(source)
        {
            Name = name;

        }




        public HeaderBind(Source source, string name, string value, bool unique) : this(source)
        {
            Name = name;
            Value = value;
            Unique = unique;
        }


        protected string Serial(object o)
        {
            return o == null ? "" : o.ToString();
        }


        internal virtual void AddHeader(RequestCreContext requestCreContext)
        {
            if (false == Enable) return;
            AddHeader(requestCreContext, this.Name, this.Value);

        }

        internal void AddHeader(RequestCreContext requestCreContext, string name, string value)
        {
            if (false == Enable) return;
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



        public override string ToString()
        {
            return "HeaderBind:" + (this.Name ?? "");
        }

        public object Clone()
        {
            HeaderBind headerBind = new HeaderBind(this.source);
            headerBind.Name = this.Name;
            headerBind.Value = this.Value;
            headerBind.Priority = this.Priority;
            headerBind.Unique = this.Unique;
            return headerBind;
        }
    }
}