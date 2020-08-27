using System.IO;
using System.Net.Http;
using System.Text;

using Feign.Core.Context;
using Feign.Core.Exception;
using Feign.Core.Reflect;
using Newtonsoft.Json;

namespace FeignCore.ValueBind
{
    internal class JsonContentBodyBind : BodyBind
    {
        internal override HttpContent Bindbody(RequestCreContext requestCreContext)
        {

            StringBuilder stringBuilder = new StringBuilder();
            Newtonsoft.Json.JsonTextWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder));
            jsonWriter.Formatting = Formatting.Indented;
            if (this.parameterWraps.Count == 1)
            {
                ParameterWrapContext parameterWrapContext = this.parameterWraps[0];
                string valueStr = parameterWrapContext.Serial(requestCreContext);
                if (valueStr == null)
                {
                    throw new FeignException("parameter value can not be null!");
                }
                if (string.IsNullOrEmpty(parameterWrapContext.Name) && TypeReflector.IsComplextClass(parameterWrapContext.Parameter.ParameterType))
                {
                    jsonWriter.WriteRaw(valueStr);
                }
                else
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName(parameterWrapContext.DataName);
                    if (valueStr != null)
                    {
                        jsonWriter.WriteRawValue(valueStr);
                    }

                    jsonWriter.WriteEndObject();
                }
            }
            else if (this.parameterWraps.Count > 1)
            {
                jsonWriter.WriteStartObject();
                this.parameterWraps.ForEach(x =>
                {
                    jsonWriter.WritePropertyName(x.DataName);
                    object value = requestCreContext.ParameterValues.Value[x.Parameter.Position];
                    string valueStr = x.Serial(value);
                    if (valueStr != null)
                    {
                        jsonWriter.WriteRawValue(valueStr);
                    }
                });
                jsonWriter.WriteEndObject();
            }
            else
            {
                // output :{}
                jsonWriter.WriteRaw("{}");

            }




            jsonWriter.Flush();
            StringContent stringContent = new StringContent(stringBuilder.ToString(), Encoding.UTF8, "application/json");

            return stringContent;
        }
    }
}