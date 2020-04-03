using Feign.Core.Attributes.RequestService;
using Feign.Core.Context;
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
    public sealed class HeaderAttribute : FeignAttribute
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
 


    }


    



}