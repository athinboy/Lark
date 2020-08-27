using System;

namespace Lark.Core.Reflect
{
    public class TypeReflector
    {

        public static TypeCodes GetTypeCodes(object o)
        {
            if (null == o) return TypeCodes.dbnull;
            return GetTypeCodes(o.GetType());

        }

        public static TypeCodes GetTypeCodes(Type type)
        {

            if (type.Equals(typeof(Int16)))
            {
                return TypeCodes.int16;
            }
            if (type.Equals(typeof(Int32)))
            {
                return TypeCodes.int32;
            }
            if (type.Equals(typeof(Int64)))
            {
                return TypeCodes.int64;
            }
            if (type.Equals(typeof(bool)))
            {
                return TypeCodes.boolean;
            }
            if (type.Equals(typeof(string)))
            {
                return TypeCodes.String;
            }
            if (type.IsPointer || type.IsAbstract || type.IsInterface || type.IsByRef || type.IsCOMObject)
            {

            }
            else
            {
                return TypeCodes.complexclass;
            }

            return TypeCodes.unsupport;
        }


        public static bool IsComplextClass(object o)
        {
            TypeCodes code = GetTypeCodes(o);
            return code == TypeCodes.complexclass;
        }
        public static bool IsComplextClass(Type type)
        {
            TypeCodes code = GetTypeCodes(type);
            return code == TypeCodes.complexclass;
        }

        public static bool IsPrivateValue(TypeCodes code)
        {
            return ((int)code > 0) && ((int)code) < 20;
        }
        public static bool IsPrivateValue(object o)
        {
            TypeCodes code = GetTypeCodes(o);
            return ((int)code > 0) && ((int)code) < 20;
        }
        public static bool IsPrivateValue(Type type)
        {
            TypeCodes code = GetTypeCodes(type);
            return ((int)code > 0) && ((int)code) < 20;
        }


    }
}