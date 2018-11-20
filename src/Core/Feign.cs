using Feign.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;


namespace Feign.Core
{


    public class Feign
    {
        private Feign()
        {

        }

        private static Dictionary<Type, object> wrapCache = new Dictionary<Type, object>();




        /// <summary>
        /// Get Feign with default config
        /// </summary>
        /// <returns></returns>
        public static Feign Default()
        {
            return new Feign();
        }

        public static T Wrap<T>() where T : class
        {



            Type interfacetype = typeof(T);

            if (wrapCache.ContainsKey(interfacetype))
            {
                return (T)wrapCache[interfacetype];
            }

            if (false == interfacetype.IsInterface)
            {
                throw new ArgumentException(string.Format(FeignResourceManager.getStr("{0} should be a interface"), nameof(T)));
            }



            Assembly assembly = interfacetype.Assembly;
            Module module = interfacetype.Module;


            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assembly.FullName), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(module.Name);
            TypeBuilder typeBuilder = moduleBuilder.DefineType(interfacetype.Name + "$1"
                 , TypeAttributes.Class | TypeAttributes.Public
                 , null
                 , new Type[] { interfacetype }
                 );

            ConstructorBuilder constructorBuilder = typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            //constructorBuilder.GetILGenerator().Emit(OpCodes.Ret);

            constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator ctor0IL = constructorBuilder.GetILGenerator();
            ctor0IL.Emit(OpCodes.Ret);



            MethodInfo[] interfacemethods = interfacetype.GetMethods();
            MethodInfo interfacemethodInfo;
            for (int i = 0; i < interfacemethods.Length; i++)
            {
                interfacemethodInfo = interfacemethods[i];
                GenerateMethod(interfacetype, interfacemethodInfo, typeBuilder);
            }

            Type newtype = typeBuilder.CreateType();


            T t = (T)Activator.CreateInstance(newtype);
            wrapCache[interfacetype] = t;

            return t;





        }

        private static void GenerateMethod(Type interfacetype, MethodInfo methodInfo, TypeBuilder typeBuilder)
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


            //test
            methodILGenerator.Emit(OpCodes.Ldarg_0);
            methodILGenerator.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("ToString", new Type[] { }));
            methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                new Type[] { typeof(string) }));

            //test
            methodILGenerator.EmitWriteLine("current method is " + methodInfo.Name);

            //test
            //methodILGenerator.Emit(OpCodes.Ldstr, "The I.M implementation of C");
            //methodILGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
            //    new Type[] { typeof(string) }));




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
            LocalBuilder methodlocal = methodILGenerator.DeclareLocal(typeof(MethodInfo));


            methodILGenerator.Emit(OpCodes.Stloc, arglistlocal.LocalIndex);// move  list of argument   to  local variable 
            methodILGenerator.Emit(OpCodes.Nop);



            // save method info 
            methodILGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            methodILGenerator.Emit(OpCodes.Stloc, methodlocal.LocalIndex);// move  methodinfo   to  local variable 




            //call proxyinvoke
            methodILGenerator.Emit(OpCodes.Ldtoken, interfacetype);
            methodILGenerator.Emit(OpCodes.Ldloc, arglistlocal.LocalIndex);
            methodILGenerator.Emit(OpCodes.Ldloc, methodlocal.LocalIndex);
            methodILGenerator.Emit(OpCodes.Call, typeof(Feign).GetMethod("ProxyInvoke", new Type[] { typeof(Type),typeof(MethodInfo), typeof(List<object>) }));



            //methodILGenerator.Emit(OpCodes.Ldloc_0);// push this object

            //methodILGenerator.Emit(OpCodes.Pop);
            methodILGenerator.Emit(OpCodes.Pop);
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

        public static object ProxyInvoke(Type interfacetype,MethodInfo methodInfo, List<Object> args)
        {

            for (int i = 0; i < args.Count; i++)
            {
                System.Console.WriteLine(args[i].ToString());
            }
            System.Console.WriteLine("hahhahahahhahahhahahha");

            return null;
        }






    }


}
