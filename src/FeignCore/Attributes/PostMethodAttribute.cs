using System.Net.Http;
using Feign.Core.Attributes;


namespace FeignCore.Attributes
{
    public class PostMethodAttribute : MethodAttribute
    {
        public PostMethodAttribute() : base(HttpMethod.Post.Method)
        {
        }

    }
}