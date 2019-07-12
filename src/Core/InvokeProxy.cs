using Feign.Core.Cache;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core
{
    public class InvokeProxy
    {


        public static object Invoke(Type interfacetype, MethodInfo methodInfo, List<Object> args)
        {
            if (false == Feign.InterfaceWrapCache.ContainsKey(interfacetype))
            {
                throw new FeignException("运行时异常：wrapcache不存在！");
            }
            InterfaceItem interfaceItem = Feign.InterfaceWrapCache[interfacetype];

            if (false == interfaceItem.MethodCache.ContainsKey(methodInfo))
            {
                throw new FeignException("运行时异常：MethodCache不存在！");
            }

            MethodItem methodItem = interfaceItem.MethodCache[methodInfo];

            //todo not implete


            HttpCreater.Create(methodItem.WrapContext,args);









            if (typeof(void) == methodInfo.ReturnType)
            {
                return null;
            }


            return null;
        }
    }
}
