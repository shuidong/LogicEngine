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
    /// <summary>
    /// 对string操作的补充
    /// </summary>
    public static partial class UtilString
    {
        #region 对象拼接成字符串和字符串分割成对象操作
        public static string Join<T>(char sep, params T[] objects)
        {
            if (objects == null || objects.Length <= 0) return "";

            StringBuilder result = new StringBuilder();
            for (int index = 0; index < objects.Length - 1; ++index)
            {
                result.Append(objects[index].ToString() + sep);
            }
            result.Append(objects[objects.Length - 1].ToString());

            return result.ToString();
        }
        public static string Join<T>(char left, char sep, char right, params T[] objects)
        {
            return left + Join<T>(sep, objects) + right;
        }
        public static string Join<T>(Func<T, string> fun, char sep, params T[] objects)
        {
            if (objects == null || objects.Length <= 0) return "";

            StringBuilder result = new StringBuilder();
            for (int index = 0; index < objects.Length - 1; ++index)
            {
                result.Append(fun(objects[index]) + sep);
            }
            result.Append(fun(objects[objects.Length - 1]));

            return result.ToString();
        }
        public static string Join<T>(Func<T, string> fun, char sep, char left, char right, params T[] objects)
        {
            return left + Join<T>(fun, sep, objects) + right;
        }
        public static T[] Split<T>(string str, char sep, Func<string, T> fun)
        {
            if (string.IsNullOrEmpty(str)) return new T[0];

            string[] splits = str.Split(sep);
            T[] result = new T[splits.Length];
            for (int index = 0; index < splits.Length; index++)
            {
                result[index] = fun(splits[index]);
            }
            return result;
        }
        public static List<T> SplitList<T>(string str, char sep, Func<string, T> fun)
        {
            if (string.IsNullOrEmpty(str)) return new List<T>();
            string[] splits = str.Split(sep);
            List<T> result = new List<T>(splits.Length);
            for (int index = 0; index < splits.Length; index++)
            {
                result.Add(fun(splits[index]));
            }
            return result;
        }
        #endregion

        #region 字符串转换成基础对象的操作
        public static Vector2i ToVector2i(string pack)
        {
            string[] splits = pack.Split(',');
            Vector2i ret;
            ret.x = Convert.ToInt32(splits[0]);
            ret.y = Convert.ToInt32(splits[1]);
            return ret;
        }
        public static Vector3i ToVector3i(string pack)
        {
            string[] splits = pack.Split(',');
            Vector3i ret;
            ret.x = Convert.ToInt32(splits[0]);
            ret.y = Convert.ToInt32(splits[1]);
            ret.z = Convert.ToInt32(splits[2]);
            return ret;
        }
        public static Vector2f ToVector2f(string pack)
        {
            string[] splits = pack.Split(',');
            Vector2f ret;
            ret.x = Convert.ToSingle(splits[0]);
            ret.y = Convert.ToSingle(splits[1]);
            return ret;
        }
        public static Vector3f ToVector3f(string pack)
        {
            string[] splits = pack.Split(',');
            Vector3f ret;
            ret.x = Convert.ToSingle(splits[0]);
            ret.y = Convert.ToSingle(splits[1]);
            ret.z = Convert.ToSingle(splits[2]);
            return ret;
        }
        public static List<Vector2i> ToVector2iList(string pack)
        {
            List<Vector2i> list = new List<Vector2i>();
            string[] seps = pack.Split(',', ';');
            bool is_x = true;
            int x = 0;
            foreach (var it in seps)
            {
                int it_2_int;
                if (int.TryParse(it, out it_2_int))
                {
                    if (is_x)
                    {
                        x = it_2_int;
                    }
                    else
                    {
                        list.Add(new Vector2i(x, it_2_int));
                    }
                    is_x = !is_x;
                }
            }
            return list;
        }
        #endregion

        public static string FillLeftWith(string str, char c, int length)
        {
            int count = length - str.Length;
            while (count > 0)
            {
                count--;
                str += c; ;
            }
            return str;
        }
        public static string MultiplicationRhymes()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= 9; ++i)
            {
                for (int j = 1; j <= i; ++j)
                {
                    var v = (i * j).ToString();
                    result.Append(i + "x" + j + "=" + v + ((v.Length == 1) ? "  " : " "));
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}