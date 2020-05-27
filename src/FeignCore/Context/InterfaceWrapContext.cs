using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;
using FeignCore.ValueBind;

namespace Feign.Core.Context
{
    internal class InterfaceWrapContext : ContextBase
    {


        public string HttpMethod = null;


        internal InterfaceWrapContext()
        {
            this.HttpMethod = DefaultConfig.DefaultHttpMethod;
        }


        public Type InterfaceType { get; set; }


        //TODO 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public Dictionary<MethodInfo, MethodItem> MethodCache { get; set; } = new Dictionary<MethodInfo, MethodItem>();

        public List<HeaderBind> HeaderBindes { get; set; } = new List<HeaderBind>();



        /// <summary>
        /// 接口URL Path. 
        /// </summary>
        public string Path { get; set; }
        public List<HeaderAttribute> HeaderAttributes { get; private set; } = new List<HeaderAttribute>();

        internal override void Clear()
        {
            throw new NotImplementedException();
        }

        internal void SaveMethod()
        {

            MethodWrapContext methodWrapContext;
            MethodInfo[] interfacemethods = this.InterfaceType.GetMethods();
            MethodInfo interfacemethodInfo;
            MethodItem methodItem;
            for (int i = 0; i < interfacemethods.Length; i++)
            {
                interfacemethodInfo = interfacemethods[i];
                methodWrapContext = MethodWrapContext.GetContext(this, interfacemethodInfo);
                methodItem = new MethodItem(interfacemethodInfo, methodWrapContext);
                this.MethodCache[interfacemethodInfo] = methodItem;
            }            
        }


        //todo add cache
        internal static InterfaceWrapContext GetContext(Type interfacetype)
        {

            InterfaceWrapContext interfaceWrapContext = new InterfaceWrapContext();

            interfaceWrapContext.InterfaceType = interfacetype;

            object[] interfaceAttibuts = interfacetype.GetCustomAttributes(true);


            BaseAttribute feignAttribute;

            for (int i = 0; i < interfaceAttibuts.Length; i++)
            {
                Object o = interfaceAttibuts[i];

                if (false == typeof(BaseAttribute).IsInstanceOfType(o))
                {
                    continue;
                }
                feignAttribute = o as BaseAttribute;
                feignAttribute.SaveToInterfaceContext(interfaceWrapContext);

            }

            interfaceWrapContext.CreateBind();

            interfaceWrapContext.SaveMethod();            

            interfaceWrapContext.Validate();

            return interfaceWrapContext;
        }


        internal override void AddHeader(RequestCreContext requestCreContext)
        {
            this.HeaderBindes.ForEach(x => { x.AddHeader(requestCreContext); });
        }

        private void Validate()
        {
        }

        internal override void CreateBind()
        {
            this.HeaderAttributes.ForEach(x =>
            {
                this.HeaderBindes.Add(new HeaderBind(x.Name, x.Value,x.Unique));
            });
        }
    }
}