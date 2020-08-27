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
                methodInfo.CallingConvention, methodInfo.ReturnType, paratypes);

            ILGenerator methodILGenerator = methodBuilder.GetILGenerator();


            // begin

            methodILGenerator.Emit(OpCodes.Nop);


            if (InternalConfig.EmitTestCode)
            {



                // output the clas name of the  proxy class 
                methodILGenerator.EmitWriteLine("");

                methodILGenerator.Emit(OpCodes.Ldstr, "Feign4Net.Core.ProxyFactory.MethodFactory Info: ");
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                    new Type[] { typeof(string) }));

                methodILGenerator.Emit(OpCodes.Ldstr, "the name of the current proxy class is: ");
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write",
                    new Type[] { typeof(string) }));

                methodILGenerator.Emit(OpCodes.Ldarg_0);
                methodILGenerator.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("GetType", new Type[] { }));
                methodILGenerator.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("get_FullName", new Type[] { }));
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                    new Type[] { typeof(string) }));

                //

                methodILGenerator.Emit(OpCodes.Ldstr, "current method is " + methodInfo.Name);
                methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                    new Type[] { typeof(string) }));

                //methodILGenerator.EmitWriteLine("current method is " + methodInfo.Name);

                //test
                //methodILGenerator.Emit(OpCodes.Ldstr, "The I.M implementation of C");
                //methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                //    new Type[] { typeof(string) }));

                methodILGenerator.EmitWriteLine("");

                //throw exception
                // methodILGenerator.Emit(OpCodes.Ldstr, "test exception");
                // methodILGenerator.Emit(OpCodes.Newobj, typeof(Exception).GetConstructor(new Type[] { typeof(String) }));
                // methodILGenerator.Emit(OpCodes.Throw);
            }



            methodILGenerator.Emit(OpCodes.Newobj, typeof(List<object>).GetConstructor(new Type[] { }));

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                if (i == 0)
                {
                    methodILGenerator.Emit(OpCodes.Dup);
                }
                EmitArgInsertList(methodILGenerator, parameterInfos[i].ParameterType, i);//todo allocate list everytime, performance  
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

            //call proxyinvoke: public static object Invoke(Type interfacetype, WrapBase wrapBase, MethodInfo methodInfo, List<Object> args)

            methodILGenerator.Emit(OpCodes.Ldtoken, interfacetype);
            methodILGenerator.EmitCall(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"), null); //parameter： interfacetype

            methodILGenerator.Emit(OpCodes.Ldarg_0); //parameter：wrapBase

            methodILGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            methodILGenerator.EmitCall(OpCodes.Call, typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle) }), null); //parameter：methodInfo

            methodILGenerator.Emit(OpCodes.Ldloc, arglistlocal.LocalIndex); //parameter：args


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

/// <summary>
/// 
/// </summary>
/// <param name="iLGenerator"></param>
/// <param name="argType"></param>
/// <param name="argIndex"></param>
        private static void EmitArgInsertList(ILGenerator iLGenerator, Type argType, int argIndex)
        {
            iLGenerator.Emit(OpCodes.Ldarg, argIndex + 1);
            if (argType.IsPrimitive)
            {
                iLGenerator.Emit(OpCodes.Box,argType);
            }
            iLGenerator.Emit(OpCodes.Callvirt, typeof(List<object>).GetMethod("Add", new Type[] { typeof(object) }));
            iLGenerator.Emit(OpCodes.Nop);

        }

    }
}