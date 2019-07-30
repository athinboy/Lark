using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Context
{
    internal class RequestCreContext
    {
        public InterfaceWrapContext InfaceContext { get; set; }
        public MethodWrapContext MethodWrap { get; set; }

        private string queryString = "";

        public string URL { get; set; } = "";

        public System.Net.Http.HttpMethod HttpMethod { get; set; } = new System.Net.Http.HttpMethod("GET");

         

    }
}
