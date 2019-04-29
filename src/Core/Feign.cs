using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Feign.Core.Cache;
using Feign.Core.Exception;
using Feign.Core.Resources;

namespace Feign.Core {

    public class Feign {
        private Feign () {

        }

        //todo 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        private static Dictionary<Type, InterfaceItem> wrapCache = new Dictionary<Type, InterfaceItem> ();

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

            Dictionary<MethodInfo, MethodItem> methodCache = new Dictionary<MethodInfo, MethodItem> ();
            MethodItem methodItem;

            Assembly assembly = interfacetype.Assembly;
            Module module = interfacetype.Module;

            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly (new AssemblyName (assembly.FullName), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule (module.Name);

            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine (interfacetype.FullName + "$1");
            // Console.WriteLine (interfacetype.Namespace+"."+interfacetype.Name + "$1");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");

            TypeBuilder typeBuilder = moduleBuilder.DefineType (interfacetype.Namespace+"."+interfacetype.Name + "$1", TypeAttributes.Class | TypeAttributes.Public, null, new Type[] { interfacetype });

            ConstructorBuilder constructorBuilder = typeBuilder.DefineDefaultConstructor (MethodAttributes.Public);
            //constructorBuilder.GetILGenerator().Emit(OpCodes.Ret);

            constructorBuilder = typeBuilder.DefineConstructor (MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator ctor0IL = constructorBuilder.GetILGenerator ();
            ctor0IL.Emit (OpCodes.Ret);

            MethodInfo[] interfacemethods = interfacetype.GetMethods ();
            MethodInfo interfacemethodInfo;
            MethodWrapContext methodWrapContext;
            InterfaceWrapContext interfaceWrapContext = InterfaceWrapContext.GetContext (interfacetype);

            for (int i = 0; i < interfacemethods.Length; i++) {
                interfacemethodInfo = interfacemethods[i];
                GenerateMethod (interfacetype, interfacemethodInfo, typeBuilder);
                methodWrapContext = MethodWrapContext.GetContext (interfaceWrapContext, interfacemethodInfo);
                methodItem = new MethodItem (interfacemethodInfo, methodWrapContext);
                methodCache[interfacemethodInfo] = methodItem;
            }

            Type newtype = typeBuilder.CreateType ();

            T t = (T) Activator.CreateInstance (newtype);
            InterfaceItem interfaceItem = new InterfaceItem (t, interfacetype, methodCache, interfaceWrapContext);

            wrapCache[interfacetype] = interfaceItem;

            return t;

        }
        /// <summary>
        /// 构造代理方法
        /// </summary>
        /// <param name="interfacetype"></param>
        /// <param name="methodInfo"></param>
        /// <param name="typeBuilder"></param>
        private static void GenerateMethod (Type interfacetype, MethodInfo methodInfo, TypeBuilder typeBuilder) {

            ParameterInfo[] parameterInfos = methodInfo.GetParameters ();
            Type[] paratypes = new Type[parameterInfos.Length];
            for (int j = 0; j < parameterInfos.Length; j++) {
                paratypes[j] = parameterInfos[j].ParameterType;
            }
            MethodBuilder methodBuilder = typeBuilder.DefineMethod (methodInfo.Name, MethodAttributes.Public | MethodAttributes.Virtual,
                CallingConventions.Standard, methodInfo.ReturnType, paratypes);

            ILGenerator methodILGenerator = methodBuilder.GetILGenerator ();

            if (InternalConfig.EmitTestCode) {

                // output the clas name of the  proxy class  
                methodILGenerator.Emit (OpCodes.Ldstr, "the name of the current proxy class is: ");
                methodILGenerator.Emit (OpCodes.Call, typeof (Console).GetMethod ("Write",
                    new Type[] { typeof (string) }));
                methodILGenerator.Emit (OpCodes.Ldarg_0);
                methodILGenerator.Emit (OpCodes.Callvirt, typeof (Object).GetMethod ("GetType", new Type[] { }));
                methodILGenerator.Emit (OpCodes.Callvirt, typeof (Type).GetMethod ("get_FullName", new Type[] { }));
                methodILGenerator.Emit (OpCodes.Call, typeof (Console).GetMethod ("WriteLine",
                    new Type[] { typeof (string) }));

                //
                methodILGenerator.EmitWriteLine ("current method is " + methodInfo.Name);

                //test
                //methodILGenerator.Emit(OpCodes.Ldstr, "The I.M implementation of C");
                //methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                //    new Type[] { typeof(string) }));
            }

            // begin

            methodILGenerator.Emit (OpCodes.Nop);

            methodILGenerator.Emit (OpCodes.Newobj, typeof (List<object>).GetConstructor (new Type[] { }));

            for (int i = 0; i < parameterInfos.Length; i++) {
                if (i == 0) {
                    methodILGenerator.Emit (OpCodes.Dup);
                }
                EmitArgInsertList (methodILGenerator, parameterInfos[i].ParameterType, i);
                if (i < parameterInfos.Length - 1) {
                    methodILGenerator.Emit (OpCodes.Dup);
                }

            }

            LocalBuilder arglistlocal = methodILGenerator.DeclareLocal (typeof (List<object>));
            //LocalBuilder methodlocal = methodILGenerator.DeclareLocal(typeof(MethodInfo));

            methodILGenerator.Emit (OpCodes.Stloc, arglistlocal.LocalIndex); // move  list of argument   to  local variable 
            methodILGenerator.Emit (OpCodes.Nop);

            //// save method info 
            //methodILGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            //methodILGenerator.Emit(OpCodes.Stloc, methodlocal.LocalIndex);// move  methodinfo   to  local variable 

            //call proxyinvoke
            methodILGenerator.Emit (OpCodes.Ldtoken, interfacetype);
            methodILGenerator.EmitCall (OpCodes.Call, typeof (Type).GetMethod ("GetTypeFromHandle"), null);
            methodILGenerator.Emit (OpCodes.Ldtoken, methodInfo);
            methodILGenerator.EmitCall (OpCodes.Call, typeof (MethodBase).GetMethod ("GetMethodFromHandle", new Type[] { typeof (RuntimeMethodHandle) }), null);
            methodILGenerator.Emit (OpCodes.Ldloc, arglistlocal.LocalIndex);

            methodILGenerator.Emit (OpCodes.Call, typeof (Feign).GetMethod ("ProxyInvoke", new Type[] { typeof (Type), typeof (MethodInfo), typeof (List<object>) }));

            if (typeof (void) == methodInfo.ReturnType) {
                methodILGenerator.Emit (OpCodes.Pop);
            } else {
                methodILGenerator.Emit (OpCodes.Castclass, methodInfo.ReturnType);
            }

            //methodILGenerator.Emit(OpCodes.Ldloc_0);// push this object

            //methodILGenerator.Emit(OpCodes.Pop);

            methodILGenerator.Emit (OpCodes.Ret);

            typeBuilder.DefineMethodOverride (methodBuilder, methodInfo);

        }

        private static void EmitArgInsertList (ILGenerator iLGenerator, Type argType, int argIndex) {
            iLGenerator.Emit (OpCodes.Ldarg, argIndex + 1);
            if (argType.IsPrimitive) {
                iLGenerator.Emit (OpCodes.Box);

            }
            iLGenerator.Emit (OpCodes.Callvirt, typeof (List<object>).GetMethod ("Add", new Type[] { typeof (object) }));
            iLGenerator.Emit (OpCodes.Nop);

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
               System.Console.WriteLine(args[i].ToString());
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