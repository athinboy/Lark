using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
namespace Feign.Core.Attributes
{

    /// <summary>
    /// http method 
    /// default: Post;
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface,
        Inherited = true, AllowMultiple = false)]
    public sealed class MethodAttribute : BaseAttribute
    {

        public string Method { get; set; } = HttpMethod.Post.Method;

        public MethodAttribute(string httpmethod)
        {
            this.Method = httpmethod.ToUpper();
        }


    }
}