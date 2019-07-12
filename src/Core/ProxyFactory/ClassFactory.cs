using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Feign.Core;
using Feign.Core.Cache;

namespace Core.ProxyFactory
{
    public class ClassFactory
    {
        public static T Wrap<T>(Type interfacetype) where T : class
        {



            Dictionary<MethodInfo, MethodItem> methodCache = new Dictionary<MethodInfo, MethodItem>();
            MethodItem methodItem;

            Assembly assembly = interfacetype.Assembly;
            Module module = interfacetype.Module;

            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assembly.FullName), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(module.Name);

            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine (interfacetype.FullName + "$1");
            // Console.WriteLine (interfacetype.Namespace+"."+interfacetype.Name + "$1");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");
            // Console.WriteLine ("11111111");

            TypeBuilder typeBuilder = moduleBuilder.DefineType(interfacetype.Namespace + "." + interfacetype.Name + "$1", TypeAttributes.Class | TypeAttributes.Public, null, new Type[] { interfacetype });

            ConstructorBuilder constructorBuilder = typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            //constructorBuilder.GetILGenerator().Emit(OpCodes.Ret);

            constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator ctor0IL = constructorBuilder.GetILGenerator();
            ctor0IL.Emit(OpCodes.Ret);

            MethodInfo[] interfacemethods = interfacetype.GetMethods();
            MethodInfo interfacemethodInfo;
            MethodWrapContext methodWrapContext;
            InterfaceWrapContext interfaceWrapContext = InterfaceWrapContext.GetContext(interfacetype);

            for (int i = 0; i < interfacemethods.Length; i++)
            {
                interfacemethodInfo = interfacemethods[i];
                MethodFactory.GenerateMethod(interfacetype, interfacemethodInfo, typeBuilder);
                methodWrapContext = MethodWrapContext.GetContext(interfaceWrapContext, interfacemethodInfo);
                methodItem = new MethodItem(interfacemethodInfo, methodWrapContext);
                methodCache[interfacemethodInfo] = methodItem;
            }

            Type newtype = typeBuilder.CreateType();

            T t = (T)Activator.CreateInstance(newtype);
            InterfaceItem interfaceItem = new InterfaceItem(t, interfacetype, methodCache, interfaceWrapContext);

            Feign.Core.Feign.InterfaceWrapCache[interfacetype] = interfaceItem;

            return t;

        }
    }
}