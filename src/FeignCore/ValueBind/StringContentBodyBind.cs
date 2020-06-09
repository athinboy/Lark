using System.Net.Http;

namespace FeignCore.ValueBind
{
    public class StringContentBodyBind : BodyBind
    {
        public override HttpContent Bindbody()
        {
            StringContent  stringContent=new StringContent("");            
            throw new System.NotImplementedException();
        }
    }
}