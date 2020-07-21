using System.Net.Http;
using Feign.Core.Attributes;


namespace FeignCore.Attributes
{
    public class GetMethodAttribute : MethodAttribute
    {
        public GetMethodAttribute() : base(HttpMethod.Get.Method)
        {
        }

    }
}