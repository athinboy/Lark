using System;
using System.Collections.Generic;
using System.Reflection;
using Feign.Core.Attributes;

namespace Feign.Core.Context
{
    internal class ReturnContext:ContextBase
    {
        private ParameterInfo parameterInfo;

        public ReturnContext(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }

        static internal ReturnContext CreContext(ParameterInfo returnParameter)
        {
            ReturnContext returnContext = new ReturnContext(returnParameter);

            IEnumerable<Attribute> attributes = returnParameter.GetCustomAttributes();
            IEnumerator<Attribute> enumerator = attributes.GetEnumerator();
            Attribute attribute;
            BaseAttribute feignAttribute;
            while (enumerator.MoveNext())
            {
                attribute = enumerator.Current;
                if (false == typeof(BaseAttribute).IsInstanceOfType(attribute))
                {
                    continue;
                }
                feignAttribute = attribute as BaseAttribute;
                feignAttribute.SaveToReturnContext(returnContext);
            }
            return returnContext;
        }

        internal override void Clear()
        {
           
        }

        internal override void CreateBind()
        {
          
        }
    }
}