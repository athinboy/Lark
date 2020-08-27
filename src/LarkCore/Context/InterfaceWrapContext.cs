using Lark.Core.Attributes;
using Lark.Core.Cache;
using Lark.Core.Enum;
using Lark.Core.ValueBind;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lark.Core.Context
{
    internal class InterfaceWrapContext : ContextBase
    {



        public string HttpMethod = null;

        public HttpContentTypes ContentType { get; set; }

        public SerializeTypes SerializeType { get; set; }


        internal InterfaceWrapContext()
        {
            HttpMethod = DefaultConfig.DefaultHttpMethod;
            ContentType = DefaultConfig.DefaultHttpContentType;
            SerializeType = DefaultConfig.DefaultSerilizeType;
        }


        public Type InterfaceType { get; set; }


        //TODO 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public Dictionary<MethodInfo, MethodItem> MethodCache { get; set; } = new Dictionary<MethodInfo, MethodItem>();

        public List<HeaderBind> HeaderBindes { get; set; } = new List<HeaderBind>();



        /// <summary>
        /// 接口URL Path. 
        /// </summary>
        public string Path { get; set; }
        public List<HeaderAttribute> HeaderAttributes { get; private set; } = new List<HeaderAttribute>();

        internal override void Clear()
        {
            throw new NotImplementedException();
        }

        internal void SaveMethod()
        {

            MethodWrapContext methodWrapContext;
            MethodInfo[] interfacemethods = InterfaceType.GetMethods();
            MethodInfo interfacemethodInfo;
            MethodItem methodItem;
            for (int i = 0; i < interfacemethods.Length; i++)
            {
                interfacemethodInfo = interfacemethods[i];
                methodWrapContext = MethodWrapContext.GetContext(this, interfacemethodInfo);
                methodItem = new MethodItem(interfacemethodInfo, methodWrapContext);
                MethodCache[interfacemethodInfo] = methodItem;
            }
        }


        //todo add cache
        internal static InterfaceWrapContext GetContext(Type interfacetype)
        {

            InterfaceWrapContext interfaceWrapContext = new InterfaceWrapContext();

            interfaceWrapContext.InterfaceType = interfacetype;

            object[] interfaceAttibuts = interfacetype.GetCustomAttributes(true);


            BaseAttribute LarkAttribute;

            for (int i = 0; i < interfaceAttibuts.Length; i++)
            {
                object o = interfaceAttibuts[i];

                if (false == typeof(BaseAttribute).IsInstanceOfType(o))
                {
                    continue;
                }
                LarkAttribute = o as BaseAttribute;
                LarkAttribute.SaveToInterfaceContext(interfaceWrapContext);

            }

            interfaceWrapContext.CreateBind();

            interfaceWrapContext.SaveMethod();

            interfaceWrapContext.Validate();

            return interfaceWrapContext;
        }




        private void Validate()
        {
        }

        internal override void CreateBind()
        {
            HeaderAttributes.ForEach(x =>
            {
                HeaderBindes.Add(new HeaderBind(HeaderBind.Source.FromInterface, x.Name, x.Value, x.Unique));
            });
        }
    }
}