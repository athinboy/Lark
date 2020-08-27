using Lark.Core.Attributes;
using Lark.Core.Context;
using Lark.Core.Exception;
using Lark.Core.ProxyFactory;
using Lark.Core.Cache;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lark.Core
{

    public class InvokeProxy
    {

        public static object Invoke(Type interfacetype, WrapBase wrapBase, MethodInfo methodInfo, List<Object> args)
        {

            // if (InternalConfig.EmitTestCode)
            // {
            //     Console.WriteLine("InvokeProxy args:");
            //     args.ForEach(x=>{
            //         Console.WriteLine((x??new object()).ToString());
            //     });
            // }

            try
            {

                if (false == Lark.InterfaceWrapCache.ContainsKey(interfacetype))
                {
                    throw new LarkException("RuntimeException：wrapcache is not exists！");
                }
                InterfaceItem interfaceItem = Lark.InterfaceWrapCache[interfacetype];

                if (false == interfaceItem.WrapContext.MethodCache.ContainsKey(methodInfo))
                {
                    throw new LarkException("RuntimeException：MethodCache is not exists！");
                }

                MethodItem methodItem = interfaceItem.WrapContext.MethodCache[methodInfo];

                //todo need a pool of RequestCreContext
                RequestCreContext requestCreContext = RequestCreContext.Create(interfaceItem.WrapContext, methodItem.WrapContext, wrapBase);
                requestCreContext.ParameterValues.Value = args;

                return HttpCreater.Create(requestCreContext).DealResponse(methodInfo.ReturnType);

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                throw ex;
            }

        }


    }
}
