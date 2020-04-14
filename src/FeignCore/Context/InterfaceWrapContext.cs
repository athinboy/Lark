using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;

namespace Feign.Core.Context
{
    internal class InterfaceWrapContext
    {
        internal bool JsonBody { get; set; } = true;

        public Type InterfaceType { get; set; }


        //todo 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public Dictionary<MethodInfo, MethodItem> MethodCache { get; set; } = new Dictionary<MethodInfo, MethodItem>();

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();

        public List<BaseAttribute> MyFeignAttributes { get; set; } = new List<BaseAttribute>();



        /// <summary>
        /// 接口URL 特性
        /// </summary>
        public URLAttribute URLAttribute { get; set; }



        public bool XmlBody { get; internal set; } = false;

        internal static void SaveMethod(InterfaceWrapContext interfaceWrapContext)
        {

            MethodWrapContext methodWrapContext;
            MethodInfo[] interfacemethods = interfaceWrapContext.InterfaceType.GetMethods();
            MethodInfo interfacemethodInfo;
            MethodItem methodItem;
            for (int i = 0; i < interfacemethods.Length; i++)
            {
                interfacemethodInfo = interfacemethods[i];
                methodWrapContext = MethodWrapContext.GetContext(interfaceWrapContext, interfacemethodInfo);
                methodItem = new MethodItem(interfacemethodInfo, methodWrapContext);
                interfaceWrapContext.MethodCache[interfacemethodInfo] = methodItem;
            }
        }


        internal static InterfaceWrapContext GetContext(Type interfacetype)
        {

            InterfaceWrapContext interfaceWrapContext = new InterfaceWrapContext();

            interfaceWrapContext.InterfaceType = interfacetype;

            object[] interfaceAttibuts = interfacetype.GetCustomAttributes(true);

        
            BaseAttribute feignAttribute;

            for (int i = 0; i < interfaceAttibuts.Length; i++)
            {


                Object o = interfaceAttibuts[i];


                if (false == typeof(BaseAttribute).IsInstanceOfType(o))
                {
                    continue;
                }
                feignAttribute = o as BaseAttribute;
                interfaceWrapContext.MyFeignAttributes.Add(feignAttribute);
                feignAttribute.SaveToInterfaceContext(interfaceWrapContext);


                if (typeof(HeaderAttribute).IsInstanceOfType(o))
                {
                    HeaderAttribute newHeader = o as HeaderAttribute; 
                    interfaceWrapContext.HeaderAttributes.Add(newHeader);
                    continue;
                }

                if (typeof(URLAttribute).IsInstanceOfType(o))
                {
                    interfaceWrapContext.URLAttribute = (o as URLAttribute);
                }

            }

            SaveMethod(interfaceWrapContext);



            interfaceWrapContext.Validate();

            return interfaceWrapContext;
        }

        private void Validate()
        {
        }

    }
}