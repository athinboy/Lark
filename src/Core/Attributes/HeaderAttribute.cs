using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// Header attribute.
    /// 
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method| AttributeTargets.Interface| AttributeTargets.Class, 
        Inherited = true, AllowMultiple = true)]
    public sealed class HeaderAttribute:Attribute
    {
        //todo need implement

        public String Header { get; set; }

        public HeaderAttribute(string header)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
        }
        





    }


}
