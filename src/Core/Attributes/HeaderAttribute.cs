using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Attributes {

    /// <summary>
    /// Header attribute.
    /// 
    /// </summary>
    [System.AttributeUsage (AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class,
        Inherited = true, AllowMultiple = true)]
    public sealed class HeaderAttribute : Attribute {
        //todo 添加值实现

        public String Name { get; set; } = "";
        public string Value { get; set; } = "";

        public HeaderAttribute (string name, string value) {
            name = (name.Trim ().Length == 0 ? null : name) ??
                throw new ArgumentNullException (nameof (name));
            value = (value.Trim ().Length == 0 ? null : value) ??
                throw new ArgumentNullException (nameof (value));
        }

    }

}