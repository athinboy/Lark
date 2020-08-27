using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Lark.Core.Context;
using Lark.Core.Reflect;
using Lark.Core.Enum;
using System.Linq;
using Lark.Core.Attributes;

namespace Lark.Core.Attributes
{

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class QueryStringAttribute : BaseAttribute
    {
        /// <summary>
        /// 对于复杂类型，如果指定了Name，则整个对象进行字符串化。否则对其进行解构。
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        public QueryStringAttribute()
        {

        }

        public QueryStringAttribute(string name)
        {
            Name = name;
        }

        internal override void Validate()
        {


        } 

        internal override void SaveToParameterContext(ParameterWrapContext parameterWrapContext)
        {
            parameterWrapContext.QueryStringAttribute=this;          
        }
 





    }
}
