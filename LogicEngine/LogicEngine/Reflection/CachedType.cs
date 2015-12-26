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
    public static class CachedType<T>
    {
        public static readonly CachedField[] Fields;

        static CachedType()
        {
            Fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public).Map(to);
        }

        static CachedField to(FieldInfo info)
        {
            return new CachedField(info);
        }

        public static string Info(T value)
        {
            StringBuilder result = new StringBuilder();
            result.Append("[" + typeof(T).Name + " ");
            if (Fields.Length > 0)
            {
                int index = 0;
                for (; index < Fields.Length - 1; ++index)
                {
                    result.Append(Fields[index].Name + "=\"" + Converter.ToStringFrom(value, Fields[index]) + "\" ");
                }
                result.Append(Fields[index].Name + "=\"" + Converter.ToStringFrom(value, Fields[index]) + "\"]");
            }
            else 
            {
                result.Append("]");
            }
            return result.ToString();
        }
    }

    public class CachedField
    {
        public FieldInfo Info { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public TypeCode Code { get; private set; }

        public CachedField(FieldInfo info)
        {
            Info = info;
            Name = info.Name;
            Type = info.FieldType;
            Code = Type.GetTypeCode(info.FieldType);
        }
    }
}