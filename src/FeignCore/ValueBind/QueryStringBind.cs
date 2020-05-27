
using Feign.Core;
using Feign.Core.Attributes.RequestService;
using Feign.Core.Context;
using Feign.Core.Reflect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FeignCore.ValueBind
{
    public class QueryStringBind:BindBase
    {

        public string Name;

        public FieldInfo Field;

        public PropertyInfo Property;
        public QueryStringBind(string name)
        {
            Name = name;  
        }

        public QueryStringBind(string name, FieldInfo field)
        {
            Name = name;
            Field = field;
        }

        public QueryStringBind(string name, PropertyInfo property)
        {
            Name = name;
            Property = property;
        }

        internal void AddParameterQueryString(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap)
        {
                      HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            object value = requestCreContext.ParaValues[parameterWrap.Parameter.Position];
            if (TypeReflector.IsPrivateValue(parameterWrap.Parameter.ParameterType))
            {


                string valueStr = parameterWrap.Serial(value);
                parameterWrap.QueryString = this.Name + "=" + valueStr;
                requestCreContext.QueryString.Add(this.Name, valueStr);
            }
            else
            {
                Dictionary<string, object> valuePairs = DeconstructUtil.Deconstruct(value);
                IEnumerator<KeyValuePair<string, object>> enumerator = valuePairs.GetEnumerator();
                KeyValuePair<string, object> keyValue;
                while (enumerator.MoveNext())
                {
                    keyValue = enumerator.Current;


                }



            }
        }
    }
}