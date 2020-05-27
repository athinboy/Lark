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


/// <summary>
/// 对于复杂类型，如果指定了Name，则整个对象进行字符串化。否则对其进行解构。
/// </summary>
/// <value></value>
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
        public bool Unique { get; set; } = DefaultConfig.HeaderUnique;

        internal override void Validate()
        {
            Name = this.Name ?? "";
            Value = ((Value ?? "").Trim().Length == 0 ? string.Empty : Value.Trim()) ?? "";

        }

        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

            if (TypeReflector.IsComplextClass(parameterItem.Parameter.ParameterType))
            {
                throw new NotSupportedException("暂时不支持复杂类型用于header、pathpara");
            }
            if(false==string.IsNullOrEmpty(this.Value)){
                throw new NotSupportedException("对于参数指定Value无效！");
            }
            parameterItem.HeaderAttributes.Add(this);             
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            methodWrapContext.HeaderAttributes.Add(this);
        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.HeaderAttributes.Add(this);
        }

    }






}