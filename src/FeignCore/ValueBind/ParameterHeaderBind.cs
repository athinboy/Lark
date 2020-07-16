using System.Reflection;
using Feign.Core.Context;

namespace FeignCore.ValueBind
{
    internal class ParameterHeaderBind : HeaderBind
    {

        internal ParameterWrapContext ParameterWrapContext { get; set; } = null;



        public ParameterHeaderBind(ParameterWrapContext parameterWrapContext) : base(HeaderBind.Source.FromParameter)
        {
            this.ParameterWrapContext = parameterWrapContext;
        }



        public FieldInfo Field;

        public PropertyInfo Property;

        public ParameterHeaderBind(ParameterWrapContext parameterWrapContext, string name, bool unique) : this(parameterWrapContext)
        {
            Name = name;
            Unique = unique;
        }



        public ParameterHeaderBind(ParameterWrapContext parameterWrapContext, string name, FieldInfo field, bool unique) : this(parameterWrapContext)
        {
            Name = name;
            this.Field = field;
            Unique = unique;
        }


        public ParameterHeaderBind(ParameterWrapContext parameterWrapContext, string name, PropertyInfo property, bool unique) : this(parameterWrapContext)
        {
            Name = name;
            this.Property = property;
            Unique = unique;
        }

        internal override void AddHeader(RequestCreContext requestCreContext)
        {
            if (false == Enable) return;
            object value = requestCreContext.ParameterValues.Value[this.ParameterWrapContext.Parameter.Position];
            AddParaValueHeader(requestCreContext, this.ParameterWrapContext, value);

        }

        private void AddParaValueHeader(RequestCreContext requestCreContext, ParameterWrapContext parameterWrap, object paraValue)
        {
            if (false == Enable) return;
            if (paraValue == null)
            {
                base.AddHeader(requestCreContext, this.Name, "");
                return;
            }

            object pValue;
            string pValueStr = Serial(paraValue);

            if (this.Field != null)
            {
                pValue = this.Field.GetValue(paraValue);
                AddHeader(requestCreContext, this.Name, pValueStr);
                return;

            }
            if (this.Property != null)
            {
                pValue = this.Property.GetValue(paraValue);
                AddHeader(requestCreContext, this.Name, pValueStr);
                return;
            }
            AddHeader(requestCreContext, this.Name, pValueStr);

        }
        public new object Clone()
        {
            ParameterHeaderBind parameterHeaderBind = new ParameterHeaderBind(this.ParameterWrapContext);
            parameterHeaderBind.Name = this.Name;
            parameterHeaderBind.Value = this.Value;
            parameterHeaderBind.Field = this.Field;
            parameterHeaderBind.Property = this.Property;
            parameterHeaderBind.Priority = this.Priority;
            parameterHeaderBind.Unique = this.Unique;
            return parameterHeaderBind;
        }


    }
}