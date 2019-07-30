using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes.RequestService
{
    internal interface IRequestConstructService
    {
        void Construct(HttpClient httpClient, HttpContent httpContent, MethodWrapContext methodWrapContext);


        void Construct(HttpClient httpClient, HttpContent httpContent, MethodWrapContext methodWrapContext, ParameterInfo parameterInfo, object para);

    }
}
