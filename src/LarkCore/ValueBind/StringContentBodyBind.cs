using System.Net.Http;
using Lark.Core.Context;
using Lark.Core.ValueBind;

namespace Lark.Core.ValueBind
{
    internal class StringContentBodyBind : BodyBind
    {
        internal override HttpContent Bindbody(RequestCreContext requestCreContext)
        {
            StringContent  stringContent=new StringContent("");            
            throw new System.NotImplementedException();
        }
    }
}