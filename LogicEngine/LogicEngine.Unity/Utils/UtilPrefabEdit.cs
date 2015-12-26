//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicEngine.Unity
{
    public static class UtilPrefabEdit
    {
        static readonly Regex regex = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);
        static readonly UTF8Encoding utf8WithoutBom = new UTF8Encoding(false);
        public static void LineReplace(string prefab_path, Func<string, string> fun)
        {
            StringReader all;
            using (var reader = new StreamReader(prefab_path))
            {
                all = new StringReader(reader.ReadToEnd());
            }
            using (var writer = new StreamWriter(prefab_path, false, utf8WithoutBom))
            {
                writer.NewLine = "\n";
                string line = all.ReadLine();
                while (line != null)
                {
                    writer.WriteLine(fun(line));
                    line = all.ReadLine();
                }
            }
        }

        /// <summary>
        /// \u2345转换成对应unicode字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string To(string str)
        {
            return regex.Replace(str, Evaluator); ;
        }
        static string Evaluator(Match m)
        {
            try
            {
                return "" + (char)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception e)
            {
                Debug.LogError(e + ":" + m.Groups[1].Value);
            }
            return m.Value;
        }
    }
}