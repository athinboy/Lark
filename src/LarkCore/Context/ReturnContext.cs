using System;
using System.Collections.Generic;
using System.Reflection;
using Lark.Core.Enum;
using Lark.Core;
using Lark.Core.Attributes;
using Lark.Core.Context;

namespace Lark.Core.Context
{
    internal class ReturnContext : ContextBase
    {
        private ParameterInfo parameterInfo;

        public ReturnContext(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
            this.SerializeType = DefaultConfig.DefaultSerilizeType;
        }


        public SerializeTypes SerializeType { get; set; }


        static internal ReturnContext CreContext(ParameterInfo returnParameter)
        {
            ReturnContext returnContext = new ReturnContext(returnParameter);

            IEnumerable<Attribute> attributes = returnParameter.GetCustomAttributes();
            IEnumerator<Attribute> enumerator = attributes.GetEnumerator();
            Attribute attribute;
            BaseAttribute LarkAttribute;
            while (enumerator.MoveNext())
            {
                attribute = enumerator.Current;
                if (false == typeof(BaseAttribute).IsInstanceOfType(attribute))
                {
                    continue;
                }
                LarkAttribute = attribute as BaseAttribute;
                LarkAttribute.SaveToReturnContext(returnContext);
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