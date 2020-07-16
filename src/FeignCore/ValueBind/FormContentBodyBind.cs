using System.Net.Http;
using Feign.Core.Context;

namespace FeignCore.ValueBind
{
    internal class FormContentBodyBind : BodyBind
    {
        internal override HttpContent Bindbody(RequestCreContext requestCreContext)
        {
            StringContent  stringContent=new StringContent("");            
            throw new System.NotImplementedException();
        }
    }
}