
using Lark.Core;
using Lark.Core.Attributes.RequestService;
using Lark.Core.Context;
using Lark.Core.Reflect;
using Lark.Core.ValueBind;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Lark.Core.ValueBind
{
    public class QueryStringBind : BindBase
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
            object paraValue = requestCreContext.ParameterValues.Value[parameterWrap.Parameter.Position];

            if (TypeReflector.IsPrivateValue(parameterWrap.Parameter.ParameterType))
            {
                //todo  valueStr :performance problem ?
                string valueStr = parameterWrap.Serial(paraValue);
                parameterWrap.QueryString = this.Name + "=" + valueStr;
                requestCreContext.QueryString.Add(this.Name, valueStr);
            }
            else
            {
                if (Field == null && Property == null)
                {
                    string valueStr = parameterWrap.Serial(paraValue);
                    parameterWrap.QueryString = this.Name + "=" + valueStr;
                    requestCreContext.QueryString.Add(this.Name, valueStr);
                }
                else
                {
                    object value;
                    string queryName = null;
                    if (Field != null)
                    {
                        queryName = Field.Name;
                        value = Field.GetValue(paraValue);
                    }
                    else if (Property != null)
                    {
                        queryName = Property.Name;
                        value = Property.GetValue(paraValue);
                    }
                    else
                    {
                        throw new SystemException(nameof(this.Name));
                    }
                    string valueStr = parameterWrap.Serial(value);
                    parameterWrap.QueryString = queryName + "=" + valueStr;
                    requestCreContext.QueryString.Add(queryName, valueStr);

                }
            }
        }
    }
}