////======================================================
//// Create by @Peng Guang Hui
//// 2015/12/3 9:40:26
////======================================================
//using System;
//using System.Collections.Generic;

//namespace LogicEngine.Utils
//{
//    static class UtilReflection
//    {
//        static byte[] ToBytes(object value, Type type, TypeCode code)
//        {
//            if (value == null)
//            {
//                return "";
//            }

//            if (IsSimpleType(code))
//            {
//                return value.ToString();
//            }

//            if (IsString(code))
//            {
//                return value as string;
//            }

//            if (IsObject(code))
//            {
//                if (type.IsArray)
//                {
//                    var array = value as Array;
//                    if (array != null && array.Length > 0)
//                    {
//                        var ele_type = type.GetElementType();
//                        var ele_code = Type.GetTypeCode(ele_type);
//                        StringBuilder result = new StringBuilder();
//                        int index = 0;
//                        for (; index < array.Length - 1; ++index)
//                        {
//                            result.Append(ToString(array.GetValue(index), ele_type, ele_code) + ';');
//                        }
//                        result.Append(ToString(array.GetValue(index), ele_type, ele_code));
//                        return result.ToString();
//                    }
//                }
//                else if (type.Equals(typeof(Vector2i)))
//                {
//                    var actual = (Vector2i)value;
//                    return actual.x.ToString() + Sep + actual.y.ToString();
//                }
//                else if (type.Equals(typeof(Vector3i)))
//                {
//                    var actual = (Vector3i)value;
//                    return actual.x.ToString() + Sep + actual.y.ToString() + Sep + actual.z.ToString();
//                }
//                else if (type.Equals(typeof(Vector2f)))
//                {
//                    var actual = (Vector2f)value;
//                    return actual.x.ToString() + Sep + actual.y.ToString();
//                }
//                else if (type.Equals(typeof(Vector3f)))
//                {
//                    var actual = (Vector3f)value;
//                    return actual.x.ToString() + Sep + actual.y.ToString() + Sep + actual.z.ToString();
//                }
//            }

//            return "";
//        }

//        public static string ToStringFrom(object holder, CachedField field)
//        {
//            return ToString(field.Info.GetValue(holder), field);
//        }
//        public static object ToObject(string value, CachedField field)
//        {
//            return ToObject(value, field.Type, field.Code);
//        }
//    }
//}