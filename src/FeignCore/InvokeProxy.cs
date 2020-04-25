using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Context;
using Feign.Core.Exception;
using Feign.Core.ProxyFactory;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core
{

    public class InvokeProxy
    {

        public static object Invoke(Type interfacetype, WrapBase wrapBase, MethodInfo methodInfo, List<Object> args)
        {

            if (false == Feign.InterfaceWrapCache.ContainsKey(interfacetype))
            {
                throw new FeignException("RuntimeException：wrapcache is not exists！");
            }
            InterfaceItem interfaceItem = Feign.InterfaceWrapCache[interfacetype];

            if (false == interfaceItem.WrapContext.MethodCache.ContainsKey(methodInfo))
            {
                throw new FeignException("RuntimeException：MethodCache is not exists！");
            }

            MethodItem methodItem = interfaceItem.WrapContext.MethodCache[methodInfo];



            RequestCreContext requestCreContext = new RequestCreContext();

            requestCreContext.InfaceContext = interfaceItem.WrapContext;

            requestCreContext.MethodWrap = methodItem.WrapContext;

            requestCreContext.WrapInstance = wrapBase;


            string resultStr = HttpCreater.Create(requestCreContext, args);


            //TODO Need to complete the return object .
            if (typeof(void) == methodInfo.ReturnType)
            {
                return null;
            }
            else if (typeof(string) == methodInfo.ReturnType)
            {
                return resultStr;
            }
            else
            {

                return resultStr;
            }

        }


    }
}
