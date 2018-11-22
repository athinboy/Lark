using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class,
    Inherited = true, AllowMultiple = false)]
    public class URLAttribute : Attribute
    {
        public URLAttribute(string url)
        {
            this.Url = url;
        }
        
        public string Url { get; set; }


    }
}
