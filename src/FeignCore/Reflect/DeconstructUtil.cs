using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Reflect
{
    public class DeconstructUtil
    {

        private static List<KeyValuePair<string, object>> nullResult = new List<KeyValuePair<string, object>>();
        public static List<KeyValuePair<string, object>> Deconstruct(Object o)
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

            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();
            for (int i = fieldInfos.Length - 1; i >= 0; i--)
            {
                values.Add(new KeyValuePair<string, object>(fieldInfos[i].Name, fieldInfos[i].GetValue(o)));
            }
            for (int i = propertyInfos.Length - 1; i >= 0; i--)
            {
                values.Add(new KeyValuePair<string, object>(propertyInfos[i].Name, propertyInfos[i].GetValue(o)));
            }

            return values;
        }
    }
}
