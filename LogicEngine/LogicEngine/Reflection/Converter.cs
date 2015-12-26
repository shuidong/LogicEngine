//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace LogicEngine
{
    public class Convert<T> where T : new()
    {
        CachedField[] mFields;
        int mByteLength;

        public Convert()
        {
            mFields = new CachedField[CachedType<T>.Fields.Length];
            CachedType<T>.Fields.CopyTo(mFields, 0);
            Array.Sort(mFields, (CachedField x, CachedField y) => x.Name.CompareTo(y.Name));
        }
        public bool TryRead(byte[] bytes, out T value)
        {
            if (bytes.Length == mByteLength)
            {
                value = Read2(bytes);
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }
        T Read2(byte[] bytes)
        {
            T value = new T();
            int start_index = 0;
            foreach (var it in mFields)
            {
                switch (it.Code)
                {
                    case TypeCode.Boolean:
                        it.Info.SetValue(value, BitConverter.ToBoolean(bytes, start_index));
                        start_index += BoolLength;
                        break;
                    case TypeCode.Byte:
                        it.Info.SetValue(value, bytes[start_index]);
                        start_index += ByteLength;
                        break;
                    case TypeCode.Char:
                        it.Info.SetValue(value, BitConverter.ToChar(bytes, start_index));
                        start_index += CharLength;
                        break;
                    case TypeCode.Double:
                        it.Info.SetValue(value, BitConverter.ToDouble(bytes, start_index));
                        start_index += DoubleLength;
                        break;
                    case TypeCode.Int16:
                        it.Info.SetValue(value, BitConverter.ToInt16(bytes, start_index));
                        start_index += Int16Length;
                        break;
                    case TypeCode.Int32:
                        it.Info.SetValue(value, BitConverter.ToInt32(bytes, start_index));
                        start_index += Int32Length;
                        break;
                    case TypeCode.Int64:
                        it.Info.SetValue(value, BitConverter.ToInt64(bytes, start_index));
                        start_index += Int64Length;
                        break;
                    case TypeCode.Single:
                        it.Info.SetValue(value, BitConverter.ToSingle(bytes, start_index));
                        start_index += SingleLength;
                        break;
                    case TypeCode.String:
                        it.Info.SetValue(value, BitConverter.ToString(bytes, start_index));
                        start_index += Int32Length;
                        break;
                    case TypeCode.UInt16:
                        it.Info.SetValue(value, BitConverter.ToUInt16(bytes, start_index));
                        start_index += Int16Length;
                        break;
                    case TypeCode.UInt32:
                        it.Info.SetValue(value, BitConverter.ToUInt32(bytes, start_index));
                        start_index += Int32Length;
                        break;
                    case TypeCode.UInt64:
                        it.Info.SetValue(value, BitConverter.ToUInt64(bytes, start_index));
                        start_index += Int64Length;
                        break;
                }
            }
            return value;
        }
        static readonly int BoolLength = BitConverter.GetBytes(true).Length;
        static readonly int ByteLength = 1;
        static readonly int CharLength = BitConverter.GetBytes('a').Length;
        static readonly int DoubleLength = BitConverter.GetBytes(0d).Length;
        static readonly int Int16Length = BitConverter.GetBytes(0u).Length;
        static readonly int Int32Length = BitConverter.GetBytes(0d).Length;
        static readonly int Int64Length = BitConverter.GetBytes(0d).Length;
        static readonly int SingleLength = BitConverter.GetBytes(0f).Length;
    }

    public static class Converter
    {
        const char Sep = ',';

        public static string ToString(object value, CachedField field)
        {
            return ToString(value, field.Type, field.Code);
        }
        static string ToString(object value, Type type, TypeCode code)
        {
            if (value == null)
            {
                return "";
            }

            if (IsSimpleType(code))
            {
                return value.ToString();
            }

            if (IsString(code))
            {
                return value as string;
            }

            if (IsObject(code))
            {
                if (type.IsArray)
                {
                    var array = value as Array;
                    if (array != null && array.Length > 0)
                    {
                        var ele_type = type.GetElementType();
                        var ele_code = Type.GetTypeCode(ele_type);
                        StringBuilder result = new StringBuilder();
                        int index = 0;
                        for (; index < array.Length - 1; ++index)
                        {
                            result.Append(ToString(array.GetValue(index), ele_type, ele_code) + ';');
                        }
                        result.Append(ToString(array.GetValue(index), ele_type, ele_code));
                        return result.ToString();
                    }
                }
                else if (type.Equals(typeof(Vector2i)))
                {
                    var actual = (Vector2i)value;
                    return actual.x.ToString() + Sep + actual.y.ToString();
                }
                else if (type.Equals(typeof(Vector3i)))
                {
                    var actual = (Vector3i)value;
                    return actual.x.ToString() + Sep + actual.y.ToString() + Sep + actual.z.ToString();
                }
                else if (type.Equals(typeof(Vector2f)))
                {
                    var actual = (Vector2f)value;
                    return actual.x.ToString() + Sep + actual.y.ToString();
                }
                else if (type.Equals(typeof(Vector3f)))
                {
                    var actual = (Vector3f)value;
                    return actual.x.ToString() + Sep + actual.y.ToString() + Sep + actual.z.ToString();
                }
            }

            return "";
        }

        public static string ToStringFrom(object holder, CachedField field)
        {
            return ToString(field.Info.GetValue(holder), field);
        }
        public static object ToObject(string value, CachedField field)
        {
            return ToObject(value, field.Type, field.Code);
        }

        static object ToObject(string value, Type type, TypeCode code)
        {
            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }

            if (type.IsArray)
            {
                var splits = value.Split(';');
                var ele_type = type.GetElementType();
                var ele_code = Type.GetTypeCode(ele_type);
                var array = Array.CreateInstance(ele_type, splits.Length);
                for (int index = 0; index < splits.Length; index++)
                {
                    var element = ToObject(splits[index], ele_type, ele_code);
                    //if (element == null) continue;
                    array.SetValue(element, index);
                }
                return array;
            }

            if (IsSimpleType(code))
            {
                return Convert.ChangeType(value, code);
            }

            if (IsString(code))
            {
                return value;
            }

            if (IsObject(code))
            {
                if (type.Equals(typeof(Vector2i)))
                {
                    int[] ints = UtilString.Split<int>(value, ',', s2i);

                    if (ints.Length >= 2)
                    {
                        return new Vector2i(ints[0], ints[1]);
                    }
                    if (ints.Length == 1)
                    {
                        return new Vector2i(ints[0], 0);
                    }
                    return new Vector2i();
                }
                else if (type.Equals(typeof(Vector3i)))
                {
                    int[] ints = UtilString.Split<int>(value, ',', s2i);

                    if (ints.Length >= 3)
                    {
                        return new Vector3i(ints[0], ints[1], ints[2]);
                    }
                    if (ints.Length == 2)
                    {
                        return new Vector3i(ints[0], ints[1], 0);
                    }
                    if (ints.Length == 1)
                    {
                        return new Vector3i(ints[0], 0, 0);
                    }
                    return new Vector3i();
                }
                else if (type.Equals(typeof(Vector2f)))
                {
                    float[] floats = UtilString.Split<float>(value, ',', s2f);

                    if (floats.Length >= 2)
                    {
                        return new Vector2f(floats[0], floats[1]);
                    }
                    if (floats.Length == 1)
                    {
                        return new Vector2f(floats[0], 0);
                    }
                    return new Vector2i();
                }
                else if (type.Equals(typeof(Vector3f)))
                {
                    float[] floats = UtilString.Split<float>(value, ',', s2f);

                    if (floats.Length >= 3)
                    {
                        return new Vector3f(floats[0], floats[1], floats[2]);
                    }
                    if (floats.Length == 2)
                    {
                        return new Vector3f(floats[0], floats[1], 0);
                    }
                    if (floats.Length == 1)
                    {
                        return new Vector3f(floats[0], 0, 0);
                    }
                    return new Vector3f();
                }
            }

            return null;
        }

        #region 内部函数
        static int s2i(string s)
        {
            int value;
            if (int.TryParse(s, out value))
            {
                return value;
            }
            return 0;
        }

        static float s2f(string s)
        {
            float value;
            if (float.TryParse(s, out value))
            {
                return value;
            }
            return 0f;
        }

        static bool IsSimpleType(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:

                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:

                case TypeCode.Char:
                case TypeCode.Double:
                case TypeCode.Single:

                case TypeCode.DateTime:
                    //case TypeCode.String:
                    //case TypeCode.Object:

                    //case TypeCode.DBNull:
                    //case TypeCode.Decimal:
                    //case TypeCode.Empty:
                    return true;
                default:
                    return false;
            }
        }

        static bool IsString(TypeCode code)
        {
            return code == TypeCode.String;
        }

        static bool IsObject(TypeCode code)
        {
            return code == TypeCode.Object;
        }
        #endregion
    }
}