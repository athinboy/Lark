using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Core.ProxyFactory;
using Feign.Core.Cache;
using Feign.Core.Exception;
using Feign.Core.ProxyFactory;

namespace Feign.Core
{

    public class Feign
    {
        private Feign()
        {

        }

        //TODO 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        internal static Dictionary<Type, InterfaceItem> InterfaceWrapCache = new Dictionary<Type, InterfaceItem>();

        /// <summary>
        /// Get Feign with default config
        /// </summary>
        /// <returns></returns>
        public static Feign Default()
        {
            return new Feign();
        }

        /// <summary>
        /// 包装接口。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //TODO 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public static T Wrap<T>(string url) where T : class
        {

            Type interfacetype = typeof(T);

            if (false == interfacetype.IsInterface)
            {
                throw new ArgumentException(string.Format("{0} should be a interface", nameof(T)));
            }

            T t;

            if (InterfaceWrapCache.ContainsKey(interfacetype))
            {
                t = (T)(InterfaceWrapCache[interfacetype].WrapInstance);
            }
            else
            {
                t = ClassFactory.Wrap<T>(interfacetype);
            }
            Type newType = t.GetType();
            Object newo= Activator.CreateInstance(newType);
            WrapBase wrapbase = (WrapBase)newo;
            T newt = (T)newo;
            wrapbase.Url = url;
            return newt;
        }





    }

}