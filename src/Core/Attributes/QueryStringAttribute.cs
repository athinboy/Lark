using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;


namespace Feign.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class QueryStringAttribute : Attribute
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public QueryStringAttribute(string name, string value)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name) ??
                throw new ArgumentNullException(nameof(name));
            Value = (value.Trim().Length == 0 ? null : value) ?? "";
        }

        public string GetQueryString()
        {
            return this.Name + "=" + this.Value;
        }

    }
}
