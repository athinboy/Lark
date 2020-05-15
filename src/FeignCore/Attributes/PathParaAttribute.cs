using System;
using System.Net.Http;
using Feign.Core.Context;
using Feign.Core.Reflect;

namespace Feign.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public class PathParaAttribute : BaseAttribute
    {
        public String Name { get; set; } = "";

        public PathParaAttribute()
        {
            this.Name = string.Empty;
        }
        public PathParaAttribute(string name)
        {
            Name = ((name ?? "").Trim().Length == 0 ? null : name.Trim()) ??
             throw new ArgumentNullException(nameof(name));
        }

        internal virtual void Validate()
        {

        }

        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {
            if(TypeReflector.IsComplextClass(parameterItem.Parameter.ParameterType)){
                throw new NotSupportedException("暂时不支持复杂类型用于header、pathpara");
            }
            parameterItem.PathParaAttribute = this;
        }

        internal void FillPath(RequestCreContext requestCreContext, ParameterWrapContext parameterWrapContext)
        {


            HttpContent httpContext = requestCreContext.httpRequestMessage.Content;
            object value = requestCreContext.ParaValues[parameterWrapContext.Parameter.Position];
            string valueStr = parameterWrapContext.Serial(value);
            requestCreContext.FillPath(this.Name, valueStr);


        }
    }




}