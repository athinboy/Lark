using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Reflect
{
    public class DeconstructUtil
    {

        private static Dictionary<string, object> nullResult = new Dictionary<string, object>();
        public static Dictionary<string, object> Deconstruct(Object o)
        {
            if (o == null)
            {
                return nullResult;
            }
            Type type = o.GetType();
            TypeReflector.GetTypeCodes(type);
            //todo is it need to cache the field/property infose?
            FieldInfo[] fieldInfos = type.GetFields();
            PropertyInfo[] propertyInfos = type.GetProperties();

            Dictionary<string, object> values = new Dictionary<string, object>();
            for (int i = fieldInfos.Length - 1; i >= 0; i--)
            {
                values.Add(fieldInfos[i].Name, fieldInfos[i].GetValue(o));
            }
            for (int i = propertyInfos.Length - 1; i >= 0; i--)
            {
                values.Add(propertyInfos[i].Name, propertyInfos[i].GetValue(o));
            }

            return values;
        }
    }
}
