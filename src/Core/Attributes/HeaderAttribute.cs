using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// Header attribute.
    /// 
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class,
        Inherited = true, AllowMultiple = true)]
    public sealed class HeaderAttribute : Attribute
    {
        //todo 添加值实现

        public String Header { get; set; }

        public HeaderAttribute(string header)
        {
            Header = (header.Trim().Length == 0 ? null : header) ?? throw new ArgumentNullException(nameof(header));
        }






    }


}
