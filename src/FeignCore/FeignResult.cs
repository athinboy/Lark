
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Feign.Core.ProxyFactory;
using System.Net;
using Feign.Core.Enum;
using System.Xml;

namespace Feign.Core
{
    internal class FeignResult
    {

        private HttpResponseMessage httpResponseMessage;

        private ReturnContext returnContext;

        internal static FeignResult GetResult(HttpResponseMessage httpResponseMessage, ReturnContext returnContext)
        {
            return new FeignResult(httpResponseMessage, returnContext);
        }

        private FeignResult(HttpResponseMessage httpResponseMessage, ReturnContext returnContext)
        {
            this.httpResponseMessage = httpResponseMessage;
            this.returnContext = returnContext;
        }

        internal object DealResponse(Type returnType)
        {
            if(httpResponseMessage==null){
                return null;
            }


            if (InternalConfig.LogResponse)
            {
                System.Console.WriteLine(this.httpResponseMessage.StatusCode.ToString());
            }

            if (typeof(void) == returnType)
            {
                return null;
            }

            Task<string> taskStr;
            taskStr = httpResponseMessage.Content.ReadAsStringAsync();
            taskStr.Wait();
            string responseString = taskStr.Result;
            if (returnType == typeof(string))
            {
                return responseString;
            }

            switch (Type.GetTypeCode(returnType))
            {

                case TypeCode.Boolean:
                    return bool.Parse(responseString);
                case TypeCode.DateTime:
                    return DateTime.Parse(responseString);
                case TypeCode.Decimal:
                    return decimal.Parse(responseString);
                case TypeCode.Int16:
                    return Int16.Parse(responseString);
                case TypeCode.Int32:
                    return Int32.Parse(responseString);
                case TypeCode.Object:
                    if (this.returnContext.SerializeType == SerializeTypes.json)
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject(responseString, returnType);
                    }
                    else if (this.returnContext.SerializeType == SerializeTypes.xml)
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(returnType);
                        return xmlSerializer.Deserialize(new XmlTextReader(responseString));

                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                default:
                    throw new NotSupportedException();

            }
            throw new NotImplementedException();
        }
    }
}