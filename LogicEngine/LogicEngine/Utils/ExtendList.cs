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

namespace LogicEngine
{
    public static class ListExtension
    {
        public static List<R> Map<T, R>(this IList<T> list, Func<T, R> fun)
        {
            List<R> result = new List<R>(list.Count);
            for (int index = 0; index < list.Count; ++index)
            {
                result.Add(fun(list[index]));
            }
            return result;
        }
        public static string Pack<T>(this IList<T> list, Func<T, string> fun, string sep)
        {
            if (list == null || list.Count <= 0) return "";

            StringBuilder result = new StringBuilder();
            for (int index = 0; index < list.Count - 1; ++index)
            {
                result.Append(fun(list[index]) + sep);
            }
            result.Append(fun(list[list.Count - 1]));

            return result.ToString();
        }
        public static string Pack<T>(this IList<T> list, string sep)
        {
            return list.Pack((T it) => it.ToString(), sep);
        }
    }
}