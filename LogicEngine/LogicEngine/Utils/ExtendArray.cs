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
    public static class ExtendArray
    {
        public static bool Contains<T>(this T[] array, T item)
        {
            for (int index = 0; index < array.Length; ++index)
            {
                if (array[index].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static R[] Map<T, R>(this T[] array, Func<T, R> fun)
        {
            R[] result = new R[array.Length];
            for (int index = 0; index < array.Length; ++index)
            {
                result[index] = fun(array[index]);
            }
            return result;
        }

        public static T[] FindAll<T>(this T[] array, Predicate<T> fun)
        {
            T[] temp = new T[array.Length];
            int count = 0;
            for (int index = 0; index < array.Length; ++index)
            {
                if (fun(array[index]))
                {
                    temp[count++] = array[index];
                }
            }
            T[] result = new T[count];
            for (int index = 0; index < count; index++)
            {
                result[index] = temp[index];
            }
            return result;
        }

        public static void Foreach<T>(this T[] array, Action<T> fun)
        {
            for (int index = 0; index < array.Length; index++)
            {
                fun(array[index]);
            }
        }
        public static T Sum<T>(this T[] array, Func<T, T, T> fun)
        {
            if (array.Length > 0)
            {
                T result = array[0];
                for (int index = 1; index < array.Length; index++)
                {
                    result = fun(result, array[index]);
                }
                return result;
            }
            return default(T);
        }
    }
}