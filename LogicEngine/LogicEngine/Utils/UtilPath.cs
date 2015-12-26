//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System.Text;

namespace LogicEngine
{
    public static class UtilPath
    {
        /// <summary>
        /// 确保路径格式在Unity上读取成功。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Normalize(string path)
        {
            StringBuilder builder = new StringBuilder(path.Length);
            foreach (var it in path.Trim())
            {
                builder.Append(it == '\\' ? '/' : it);
            }
            return builder.ToString();
        }


        public static string Combine(string prefix, string name)
        {
            prefix = Normalize(prefix);
            name = Normalize(name);

            if (IsSeparator(prefix[prefix.Length - 1]))
            {
                if (IsSeparator(name[0]))
                {
                    name = name.Substring(1);
                }
            }
            else
            {
                if (!IsSeparator(name[0]))
                {
                    name = "/" + name;
                }
            }
            return prefix + name;
        }

        public static string Combine(string prefix, string name, params string[] names)
        {
            string ppp = Combine(prefix, name);
            foreach (var it in names)
            {
                ppp = Combine(ppp, it);
            }
            return ppp;
        }

        public static bool IsSeparator(char c)
        {
            return c == '/';
        }
    }
}