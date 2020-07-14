using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Enum;
using Feign.Core.Reflect;
using FeignCore.ValueBind;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Feign.Core.Context
{
    internal class ParameterWrapContext : ContextBase
    {
        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();
        public List<HeaderBind> HeaderBindes { get; set; } = new List<HeaderBind>();
        public List<QueryStringBind> QueryStringBindes { get; set; } = new List<QueryStringBind>();

        public QueryStringAttribute QueryStringAttribute { get; set; } = null;

        public MethodWrapContext MethodWrap { get; set; }

        public ParameterInfo Parameter { get; set; }

        public bool AttributeBinded { get; set; } = false;


        public bool IsBody { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string QueryString { get; internal set; } = string.Empty;

        public PathParaAttribute pathParaAttribute;

        public List<PathParaBind> PathParaBindes { get; internal set; } = new List<PathParaBind>();


        private ParameterWrapContext()
        {

        }
        public ParameterWrapContext(MethodWrapContext methodWrapContext, ParameterInfo parameter) : this()
        {
            this.Parameter = parameter;
            this.MethodWrap = methodWrapContext;

        }

        internal override void Clear()
        {
            this.PathParaBindes = null;
            this.HeaderBindes.Clear();
            this.QueryStringBindes.Clear();
            this.SerializeType = SerializeTypes.json;
            IsBody = false;
            this.QueryString = string.Empty;
            throw new NotImplementedException();
        }


        private void BindAttribute()
        {


            PathParaBind paraBind;
            HeaderBind headerBind;
            QueryStringBind queryStringBind;
            string headername;
            List<FieldInfo> fieldInfos = new List<FieldInfo>(this.Parameter.ParameterType.GetFields());
            List<PropertyInfo> properties = new List<PropertyInfo>(this.Parameter.ParameterType.GetProperties());

            if (this.pathParaAttribute != null)
            {
                paraBind = new PathParaBind(string.IsNullOrEmpty(pathParaAttribute.Name) ? this.Parameter.Name : pathParaAttribute.Name);
                paraBind.Prompt();
                this.PathParaBindes.Add(paraBind);
                AttributeBinded = true;
            }
            if (this.HeaderAttributes.Count > 0)
            {
                this.HeaderAttributes.ForEach(x =>
                {
                    if (TypeReflector.IsPrivateValue(this.Parameter.ParameterType))
                    {
                        headername = string.IsNullOrEmpty(x.Name) ? this.Parameter.Name : x.Name;
                        headerBind = new HeaderBind(headername, x.Unique);
                        headerBind.Prompt();
                        this.HeaderBindes.Add(headerBind);
                    }
                    else
                    {
                        fieldInfos.ForEach(f =>
                        {
                            headerBind = new HeaderBind(f.Name, f, x.Unique);
                            headerBind.Prompt();
                            this.HeaderBindes.Add(headerBind);
                        });
                        properties.ForEach(p =>
                        {
                            headerBind = new HeaderBind(p.Name, p, x.Unique);
                            headerBind.Prompt();
                            this.HeaderBindes.Add(headerBind);
                        });

                    }
                });
                AttributeBinded = true;
            }
            if (this.QueryStringAttribute != null)
            {
                string queryname;
                if (TypeReflector.IsPrivateValue(this.Parameter.ParameterType))
                {
                    queryname = string.IsNullOrEmpty(this.QueryStringAttribute.Name) ? this.Parameter.Name : this.QueryStringAttribute.Name;
                    queryStringBind = new QueryStringBind(queryname);
                    queryStringBind.Prompt();
                    this.QueryStringBindes.Add(queryStringBind);
                }
                else
                {

                    if (false == string.IsNullOrEmpty(this.QueryStringAttribute.Name))
                    {
                        queryStringBind = new QueryStringBind(this.QueryStringAttribute.Name);
                        queryStringBind.Prompt();
                        this.QueryStringBindes.Add(queryStringBind);
                    }
                    else
                    {
                        fieldInfos.ForEach(f =>
                        {
                            queryStringBind = new QueryStringBind(f.Name, f);
                            queryStringBind.Prompt();
                            this.QueryStringBindes.Add(queryStringBind);
                        });
                        properties.ForEach(p =>
                        {
                            queryStringBind = new QueryStringBind(p.Name, p);
                            queryStringBind.Prompt();
                            this.QueryStringBindes.Add(queryStringBind);
                        });
                    }

                }
                AttributeBinded = true;
            }         


        }


        private void PresumeBind()
        {
            if (true == AttributeBinded)
            {
                return;
            }
            //bind to path para
            if (this.PresumePathParaBind())
            {
                return;
            }
            else
            {
                //bind to querystring
                this.PresumeQueryStringBind();
            }
            if (this.MethodWrap.IsGet())
            {
                return;
            }


            if (this.HeaderBindes.Count > 0 || this.PathParaBindes.Count > 0 || this.QueryStringBindes.Count > 0)
            {
                return;
            }
            else
            {
                //bind to body

            }

        }

        private bool PresumePathParaBind()
        {

            PathParaBind paraBind;
            if (TypeReflector.IsPrivateValue(this.Parameter.ParameterType))
            {
                paraBind = this.MethodWrap.GetPathParaBind(this.Parameter.Name);
                if (paraBind == null)
                {
                    paraBind = new PathParaBind(this.Parameter.Name);
                    this.PathParaBindes.Add(paraBind);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool PresumeQueryStringBind()
        {
            QueryStringBind queryStringBind;
            List<FieldInfo> fieldInfos = new List<FieldInfo>(this.Parameter.ParameterType.GetFields());
            List<PropertyInfo> properties = new List<PropertyInfo>(this.Parameter.ParameterType.GetProperties());
            if (this.MethodWrap.IsGet())
            {
                if (TypeReflector.IsPrivateValue(this.Parameter.ParameterType))
                {
                    queryStringBind = new QueryStringBind(this.Parameter.Name);
                    this.QueryStringBindes.Add(queryStringBind);
                    return true;
                }
                else
                {
                    fieldInfos.ForEach(f =>
                    {
                        queryStringBind = new QueryStringBind(f.Name, f);
                        this.QueryStringBindes.Add(queryStringBind);
                    });
                    properties.ForEach(p =>
                    {
                        queryStringBind = new QueryStringBind(p.Name, p);
                        this.QueryStringBindes.Add(queryStringBind);
                    });
                    return true;
                }

            }
            if (this.MethodWrap.IsPOST())
            {
                return false;
            }
            return false;
        }



        internal override void CreateBind()
        {
            PresumeBind();
        }


        internal static ParameterWrapContext GetContext(MethodWrapContext methodWrapContext, ParameterInfo parameterInfo)
        {

            MethodInfo methodInfo = methodWrapContext.methodInfo;

            Attribute attribute;
            ParameterWrapContext parameterContext;
            BaseAttribute feignAttribute;



            parameterContext = new ParameterWrapContext(methodWrapContext, parameterInfo);
            IEnumerable<Attribute> attributes = parameterInfo.GetCustomAttributes();
            IEnumerator<Attribute> enumerator = attributes.GetEnumerator();

            while (enumerator.MoveNext())
            {
                attribute = enumerator.Current;
                if (false == typeof(BaseAttribute).IsInstanceOfType(attribute))
                {
                    continue;
                }
                feignAttribute = attribute as BaseAttribute;

                feignAttribute.SaveToParameterContext(parameterContext);
            }

            parameterContext.BindAttribute();
            return parameterContext;

        }




        internal void FillPath(RequestCreContext requestCreContext)
        {
            if (this.PathParaBindes != null)
            {

                this.PathParaBindes.ForEach(x =>
                {
                    x.FillPath(requestCreContext, this);
                });



            }
        }

        internal override void AddHeader(RequestCreContext requestCreContext)
        {
            this.HeaderBindes.ForEach(x =>
            {
                x.AddParameterHeader(requestCreContext, this);
            });

        }

        internal override void AddQueryString(RequestCreContext requestCreContext)
        {
            this.QueryStringBindes.ForEach(x =>
            {
                x.AddParameterQueryString(requestCreContext, this);
            });
        }


        internal string Serial(object value)
        {

            if (value == null)
            {
                return string.Empty;
            }
            Type type = value.GetType();

            if (type.IsValueType)
            {
                return value.ToString();
            }
            if (typeof(string).IsInstanceOfType(value))
            {
                return value.ToString();
            }
            if (type.IsPrimitive)
            {
                return value.ToString();

            }
            if (this.SerializeType == SerializeTypes.tostring)
            {
                return value.ToString();
            }
            if (this.SerializeType == SerializeTypes.xml)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);
                xmlSerializer.Serialize(stringWriter, value);
                return stringBuilder.ToString();
            }
            if (this.SerializeType == SerializeTypes.json)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
            return value.ToString();



        }


    }
}
