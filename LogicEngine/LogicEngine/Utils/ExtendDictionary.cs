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

namespace LogicEngine
{
    public static class ExtendDictionary
    {
        public static void ForeachKey<K, V>(this Dictionary<K, V> dict, Action<K> fun)
        {
            foreach (var it in dict)
            {
                fun(it.Key);
            }
        }
        public static void ForeachValue<K, V>(this Dictionary<K, V> dict, Action<V> fun)
        {
            foreach (var it in dict)
            {
                fun(it.Value);
            }
        }
        public static List<R> MapValue<K, V, R>(this Dictionary<K, V> dict, Func<V, R> fun)
        {
            List<R> result = new List<R>(dict.Count);
            foreach (var it in dict)
            {
                result.Add(fun(it.Value));
            }
            return result;
        }
    }
}