using Feign.Core.Attributes.RequestService;
using Feign.Core.Cache;
using Feign.Core.Context;
using FeignCore.Serialize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// xml serialize.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class XmlAttribute : BaseAttribute
    {


        internal override void SaveToParameterContext(ParameterWrapContext parameterWrapContext)
        {
            base.SaveToParameterContext(parameterWrapContext);
            parameterWrapContext.serializeType=SerializeTypes.xml;
        }



    }
}
