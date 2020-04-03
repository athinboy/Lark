using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Feign.Core;
using Feign.Core.ProxyFactory;

namespace Core.ProxyFactory
{
    public class MethodFactory
    {
        /// <summary>
        /// 构造代理方法
        /// </summary>
        /// <param name="interfacetype"></param>
        /// <param name="methodInfo"></param>
        /// <param name="typeBuilder"></param>
        public static void GenerateMethod(Type interfacetype, MethodInfo methodInfo, TypeBuilder typeBuilder)
        {

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            Type[] paratypes = new Type[parameterInfos.Length];
            for (int j = 0; j < parameterInfos.Length; j++)
            {
                paratypes[j] = parameterInfos[j].ParameterType;
            }
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public | MethodAttributes.Virtual,
                CallingConventions.Standard, methodInfo.ReturnType, paratypes);

            ILGenerator methodILGenerator = methodBuilder.GetILGenerator();

            if (InternalConfig.EmitTestCode)
            {

                // output the clas name of the  proxy class  
                methodILGenerator.Emit(OpCodes.Ldstr, "the name of the current proxy class is: ");
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write",
                    new Type[] { typeof(string) }));
                methodILGenerator.Emit(OpCodes.Ldarg_0);
                methodILGenerator.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("GetType", new Type[] { }));
                methodILGenerator.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("get_FullName", new Type[] { }));
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                    new Type[] { typeof(string) }));

                //
                methodILGenerator.EmitWriteLine("current method is " + methodInfo.Name);

                //test
                //methodILGenerator.Emit(OpCodes.Ldstr, "The I.M implementation of C");
                //methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                //    new Type[] { typeof(string) }));
            }

            // begin

            methodILGenerator.Emit(OpCodes.Nop);

            methodILGenerator.Emit(OpCodes.Newobj, typeof(List<object>).GetConstructor(new Type[] { }));

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                if (i == 0)
                {
                    methodILGenerator.Emit(OpCodes.Dup);
                }
                EmitArgInsertList(methodILGenerator, parameterInfos[i].ParameterType, i);
                if (i < parameterInfos.Length - 1)
                {
                    methodILGenerator.Emit(OpCodes.Dup);
                }

            }

            LocalBuilder arglistlocal = methodILGenerator.DeclareLocal(typeof(List<object>));
            //LocalBuilder methodlocal = methodILGenerator.DeclareLocal(typeof(MethodInfo));

            methodILGenerator.Emit(OpCodes.Stloc, arglistlocal.LocalIndex); // move  list of argument   to  local variable 
            methodILGenerator.Emit(OpCodes.Nop);

            //// save method info 
            //methodILGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            //methodILGenerator.Emit(OpCodes.Stloc, methodlocal.LocalIndex);// move  methodinfo   to  local variable 

            //call proxyinvoke
            methodILGenerator.Emit(OpCodes.Ldtoken, interfacetype);
            methodILGenerator.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null);

            methodILGenerator.Emit(OpCodes.Ldarg_0);

            methodILGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            methodILGenerator.EmitCall(OpCodes.Call, typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle) }), null);
            methodILGenerator.Emit(OpCodes.Ldloc, arglistlocal.LocalIndex);

            methodILGenerator.Emit(OpCodes.Call, typeof(Feign.Core.InvokeProxy).GetMethod("Invoke",
                new Type[] { typeof(Type), typeof(WrapBase), typeof(MethodInfo), typeof(List<object>) }));

            if (typeof(void) == methodInfo.ReturnType)
            {
                methodILGenerator.Emit(OpCodes.Pop);
            }
            else
            {
                methodILGenerator.Emit(OpCodes.Castclass, methodInfo.ReturnType);
            }

            //methodILGenerator.Emit(OpCodes.Ldloc_0);// push this object

            //methodILGenerator.Emit(OpCodes.Pop);

            methodILGenerator.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);

        }

        private static void EmitArgInsertList(ILGenerator iLGenerator, Type argType, int argIndex)
        {
            iLGenerator.Emit(OpCodes.Ldarg, argIndex + 1);
            if (argType.IsPrimitive)
            {
                iLGenerator.Emit(OpCodes.Box);

            }
            iLGenerator.Emit(OpCodes.Callvirt, typeof(List<object>).GetMethod("Add", new Type[] { typeof(object) }));
            iLGenerator.Emit(OpCodes.Nop);

        }

    }
}