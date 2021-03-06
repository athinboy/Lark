﻿using Lark.Core.Attributes;
using Lark.Core.Enum;
using Lark.Core.Exception;
using Lark.Core.ValueBind;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lark.Core.Context
{
    internal sealed class MethodWrapContext : ContextBase
    {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }



        private HttpContentTypes contentType = HttpContentTypes.none;

        public SerializeTypes serializeType = SerializeTypes.none;


        public SerializeTypes SerializeType
        {
            get
            {
                return this.serializeType == SerializeTypes.none ? this.interfaceWrapContext.SerializeType : this.serializeType;
            }
            set
            {
                this.serializeType = value;
            }
        }


        internal HttpContentTypes ContentType
        {
            get
            {
                return this.contentType == HttpContentTypes.none ? this.interfaceWrapContext.ContentType : this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }


        private List<string> pathParaNames = null;
        public List<string> PathParaNames
        {
            get
            {
                if (pathParaNames == null)
                {
                    pathParaNames = new List<string>(Util.GetPathParaName(this.MethodPath));
                }
                return pathParaNames;
            }

        }



        private string httpMethod = null;

        public string HttpMethod
        {
            get
            {
                return this.httpMethod ?? this.interfaceWrapContext.HttpMethod;
            }
            set
            {
                this.httpMethod = value;
            }

        }



        private string methodPath = null;

        /// <summary>
        /// URL Path.
        /// </summary>
        public string Path { get; set; }

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();


        public List<HeaderBind> HeaderBindes { get; set; } = new List<HeaderBind>();

        public List<ParameterWrapContext> ParameterCache { get; set; } = new List<ParameterWrapContext>();

        public BodyBind bodyBind = null;

        /// <summary>
        /// 方法的URL
        /// </summary>
        public PathAttribute MethodPathAttribute { get; set; }

        private InterfaceWrapContext interfaceWrapContext = null;

        public ReturnContext ReturnContext { get; private set; }

        internal override void Clear()
        {
            throw new NotImplementedException();
        }

        private void SaveParameter()
        {
            ParameterInfo parameter;
            ParameterInfo[] parameterInfos = this.methodInfo.GetParameters();
            ParameterWrapContext parameterWrap;
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                parameter = parameterInfos[i];
                parameterWrap = ParameterWrapContext.GetContext(this, parameter);
                ParameterCache.Add(parameterWrap);
            }
            ParameterCache.ForEach(x =>
            {
                x.CreateBind();
            });

        }


        internal static MethodWrapContext GetContext(InterfaceWrapContext interfaceWrapContext, MethodInfo methodInfo)
        {
            MethodWrapContext methodWrapContext = new MethodWrapContext();
            methodWrapContext.interfaceType = interfaceWrapContext.InterfaceType;
            methodWrapContext.methodInfo = methodInfo;
            methodWrapContext.interfaceWrapContext = interfaceWrapContext;
            methodWrapContext.ReturnContext = ReturnContext.CreContext(methodInfo.ReturnParameter);


            object[] cas = methodInfo.GetCustomAttributes(true);
            BaseAttribute LarkAttribute;

            object ca;
            List<string> headers = new List<string>();
            for (int i = 0; i < cas.Length; i++)
            {
                ca = cas[i];
                if (false == typeof(BaseAttribute).IsInstanceOfType(ca))
                {

                    continue;
                }
                LarkAttribute = ca as BaseAttribute;
                LarkAttribute.SaveToMethodContext(methodWrapContext);
            }

            methodWrapContext.CreateBind();

            methodWrapContext.SaveParameter();

            methodWrapContext.Validate();

            return methodWrapContext;

        }

        private void PresumeBodyBind()
        {
            if (this.ContentType == HttpContentTypes.formdata)
            {
                this.bodyBind = new FormContentBodyBind();
            }
            if (this.ContentType == HttpContentTypes.textstr)
            {
                this.bodyBind = new StringContentBodyBind();
            }
            if (this.ContentType == HttpContentTypes.json)
            {
                this.bodyBind = new JsonContentBodyBind();
            }
            if (this.ContentType == HttpContentTypes.xml)
            {
                this.bodyBind = new XmlContentBodyBind();
            }

        }

        internal override void CreateBind()
        {
            this.HeaderAttributes.ForEach(x =>
            {
                this.HeaderBindes.Add(new HeaderBind(HeaderBind.Source.FromMethod, x.Name, x.Value, x.Unique));
            });
            if (this.IsPOST())
            {
                this.PresumeBodyBind();
            }
        }

        private void Validate()
        {

            if (this.MethodPathAttribute == null
                && string.IsNullOrEmpty(this.HttpMethod)
                && string.IsNullOrEmpty(this.interfaceWrapContext.Path))
            {
                throw new LarkException("URL and Method Attribute can not be both Null ！");
            }
            if (false == DefaultConfig.SupportHttpMethod.Contains(this.HttpMethod))
            {
                throw new LarkException("Just support GET/POST now！");
            }
            this.ValidateParameterBind();

        }


        //todo need to complete
        private void ValidateParameterBind()
        {

            ParameterWrapContext parameterWrapContextA;
            ParameterWrapContext parameterWrapContextB;
            for (int i = 0; i < this.ParameterCache.Count; i++)
            {
                parameterWrapContextA = ParameterCache[i];
                for (int j = i + 1; j < this.ParameterCache.Count; j++)
                {
                    parameterWrapContextB = this.ParameterCache[j];

                    parameterWrapContextA.HeaderBindes.ForEach(x =>
                    {
                        parameterWrapContextB.HeaderBindes.ForEach(y =>
                        {
                        });

                    });
                    parameterWrapContextA.QueryStringBindes.ForEach(x =>
                    {
                        parameterWrapContextB.QueryStringBindes.ForEach(y =>
                        {
                            if (x.Name == y.Name)
                            {
                                throw new System.Exception(string.Format("参数:{0}、{1}具有相同的查询参数:{2}", parameterWrapContextA.Parameter.Name, parameterWrapContextB.Parameter.Name, x.Name));
                            }
                        });

                    });
                }

            }



        }

        public PathParaBind GetPathParaBind(string paraName)
        {
            paraName = paraName.ToLower();          
            ParameterWrapContext parameterWrapContext;
            PathParaBind pathParaBind;
            for (int i = 0; i < this.ParameterCache.Count; i++)
            {
                parameterWrapContext = ParameterCache[i];
                for (int j = 0; j < parameterWrapContext.PathParaBindes.Count; j++)
                {
                    pathParaBind = parameterWrapContext.PathParaBindes[j];
                    if (paraName.ToLower() == pathParaBind.Name)
                    {
                        return pathParaBind;
                    }

                }

            }
            return null;


        }


        public bool NeedPathPara(string paraName)
        {
            paraName = paraName.ToLower(); 
            return this.PathParaNames.Contains(paraName);
        }



        public string MethodPath
        {
            get
            {
                if (methodPath == null)
                {
                    methodPath = Util.NormalizeURL(this.interfaceWrapContext.Path ?? "") + Util.NormalizeURL(this.Path ?? "");
                }
                return methodPath;
            }
            private set { }


        }


        public bool IsGet()
        {
            return this.HttpMethod == "GET";
        }
        public bool IsPOST()
        {
            return this.HttpMethod == "POST";
        }





    }
}