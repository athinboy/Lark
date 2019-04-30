using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Core.ProxyFactory;
using Feign.Core.Cache;
using Feign.Core.Exception;
using Feign.Core.Resources;

namespace Feign.Core {

    public class Feign {
        private Feign () {

        }

        //todo 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        internal  static Dictionary<Type, InterfaceItem> wrapCache = new Dictionary<Type, InterfaceItem> ();

        /// <summary>
        /// Get Feign with default config
        /// </summary>
        /// <returns></returns>
        public static Feign Default () {
            return new Feign ();
        }

        /// <summary>
        /// 包装接口。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //todo 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public static T Wrap<T> (string url) where T : class {

            Type interfacetype = typeof (T);

            if (wrapCache.ContainsKey (interfacetype)) {
                return (T) (wrapCache[interfacetype].WrapInstance);
            }

            if (false == interfacetype.IsInterface) {
                throw new ArgumentException (string.Format (FeignResourceManager.getStr ("{0} should be a interface"), nameof (T)));
            }
            return ClassFactory.Wrap<T>(interfacetype);


        }



        public static object ProxyInvoke (Type interfacetype, MethodInfo methodInfo, List<Object> args) {
            if (false == wrapCache.ContainsKey (interfacetype)) {
                throw new FeignException ("运行时异常：wrapcache不存在！");
            }
            InterfaceItem interfaceItem = wrapCache[interfacetype];

            if (false == interfaceItem.MethodCache.ContainsKey (methodInfo)) {
                throw new FeignException ("运行时异常：MethodCache不存在！");
            }

            MethodItem methodItem = interfaceItem.MethodCache[methodInfo];

            //todo not implete

            for (int i = 0; i < args.Count; i++)
            {
                if(InternalConfig.EmitTestCode)
                {
                    System.Console.WriteLine(args[i].ToString());
                }
               
            }
            System.Console.WriteLine("hahhahahahhahahhahahha");

             

            if (typeof (void) == methodInfo.ReturnType) {
                return null;
            }
            HttpClient httpClient = new HttpClient ();

            return null;
        }

    }

}