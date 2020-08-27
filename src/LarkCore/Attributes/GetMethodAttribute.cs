using System.Net.Http;
using Lark.Core.Attributes;


namespace Lark.Core.Attributes
{
    public class GetMethodAttribute : MethodAttribute
    {
        public GetMethodAttribute() : base(HttpMethod.Get.Method)
        {
        }

    }
}