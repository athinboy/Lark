using System;
using System.Collections.Generic;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Exception;

namespace Feign.Core
{
    internal class InterfaceWrapContext
    {
        public Type interfaceType { get; set; }

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();

        /// <summary>
        /// parent context.
        /// </summary>
        public InterfaceWrapContext ParentContext { get; set; } = null;



        /// <summary>
        /// 接口URL 特性
        /// </summary>
        public URLAttribute URLAttribute { get; set; }

        internal static InterfaceWrapContext GetContext<T>(T t)
        {

            Type interfacetype = typeof(T);

            InterfaceWrapContext interfaceWrapContext = new InterfaceWrapContext();

            interfaceWrapContext.interfaceType = interfacetype;

            object[] interfaceAttibuts = interfacetype.GetCustomAttributes(true);

            List<string> headers = new List<string>();

            for (int i = 0; i < interfaceAttibuts.Length; i++)
            {
                Object o = interfaceAttibuts[i];
                if (typeof(HeaderAttribute).IsInstanceOfType(o))
                {
                    HeaderAttribute newHeader = o as HeaderAttribute;

                    if (headers.Contains(newHeader.Name))
                    {
                        throw new FeignException("接口:{0}重复的Header Name:{1}", interfacetype.Name, newHeader.Name);
                    }
                    else
                    {
                        headers.Add(newHeader.Name);

                    }
                    interfaceWrapContext.HeaderAttributes.Add(newHeader);
                    continue;
                }

                if (typeof(URLAttribute).IsInstanceOfType(o))
                {
                    interfaceWrapContext.URLAttribute = (o as URLAttribute);
                }

            }

            interfaceWrapContext.Validate();

            return interfaceWrapContext;
        }

        private void Validate()
        {

        }

    }
}