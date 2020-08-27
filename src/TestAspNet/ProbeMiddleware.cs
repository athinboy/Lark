using Lark.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestInterface;

namespace TestAspNet
{
    ///<summary>
    /// set response header "probeInfo", for debug propose 。      
    ///</summary>
    public class ProbeMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();

            HttpRequest httpRequest = context.Request;
            HttpResponse httpResponse = context.Response;



            ProbeInfo probeInfo = new ProbeInfo();

            IHeaderDictionary headers = httpRequest.Headers;

            probeInfo.Url = httpRequest.Host + httpRequest.Path.Value + httpRequest.QueryString;

            probeInfo.Method = httpRequest.Method;

            foreach (string key in headers.Keys)
            {
                probeInfo.Headers.Add(new KeyValuePair<string, string>(key, headers[key]));
            }
            if (null != httpRequest.ContentLength)
            {
                long conlength = httpRequest.ContentLength.Value;
                byte[] conBytes = new byte[conlength];
                Stream bodyStream = httpRequest.Body;
                long c = 1;
                long index = 0;


                while (c > 0 && (conlength - index) > 0)
                {
                    c = bodyStream.Read(conBytes, int.Parse(index.ToString()), int.Parse((conlength - index).ToString()));
                    index += c;

                }
                string constr = System.Text.UTF8Encoding.UTF8.GetString(conBytes);
                probeInfo.Body = constr;
                bodyStream.Position = 0;
            }

            httpResponse.Headers.Add("probeInfo", System.Text.Encodings.Web.HtmlEncoder.Default.Encode(Newtonsoft.Json.JsonConvert.SerializeObject(probeInfo)));



            return next.Invoke(context);

        }
    }
}
