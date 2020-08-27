using System;
using System.Collections.Generic;
using System.Net.Http;
using Feign.Core.Context;
using Feign.Core.Reflect;

namespace FeignCore.ValueBind
{
    internal class FormContentBodyBind : BodyBind
    {
        internal override HttpContent Bindbody(RequestCreContext requestCreContext)
        {
            List<KeyValuePair<String, String>> keyValues = new List<KeyValuePair<String, String>>();
            if (this.parameterWraps.Count == 0)
            {

            }
            this.parameterWraps.ForEach(x =>
            {
                string valueStr;
                if (TypeReflector.IsComplextClass(x.Parameter.ParameterType))
                {
                    List<KeyValuePair<string, object>> valuePairs =
                     DeconstructUtil.Deconstruct(requestCreContext.ParameterValues.Value[x.Parameter.Position]);
                    valuePairs.ForEach(kp =>
                    {
                        valueStr = x.Serial(kp.Value);
                        keyValues.Add(new KeyValuePair<string, string>(kp.Key, valueStr));
                    });

                }
                else
                {
                    valueStr = x.Serial(requestCreContext);
                    valueStr = valueStr ?? "";
                    keyValues.Add(new KeyValuePair<string, string>(x.DataName, valueStr));
                }

            });

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(keyValues);
            return formContent;
        }
    }
}