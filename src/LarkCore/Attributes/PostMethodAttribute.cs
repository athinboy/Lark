using System.Net.Http;
using Lark.Core.Attributes;


namespace Lark.Core.Attributes
{
    public class PostMethodAttribute : MethodAttribute
    {
        public PostMethodAttribute() : base(HttpMethod.Post.Method)
        {
        }

    }
}