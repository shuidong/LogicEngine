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
using Newtonsoft.Json;

namespace LogicEngine
{
    public static class UtilJson
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T To<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch { return default(T); }
        }
        public static object To(string json, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(json, type);
            }
            catch { return null; }
        }
    }
}