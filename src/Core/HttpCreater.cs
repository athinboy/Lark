
using Feign.Core.Attributes;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core
{
    internal class HttpCreater
    {
        static void Create(MethodWrapContext mwContext)
        {
            if (mwContext == null)
            {
                throw new ArgumentNullException(nameof(mwContext));
            }
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

        }
    }
}

