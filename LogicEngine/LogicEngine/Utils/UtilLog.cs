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
    public static class UtilLog
    {
        public static void LogEntity(Entity entity)
        {
            Logic.Adapter.Log(UtilDebug.GetCallerInfo(0) + " " + entity.ToTree());
        }

        public static void Log(string message)
        {
            Logic.Adapter.Log(UtilDebug.GetCallerInfo(0) + " " + message);
        }
        public static void LogFormat(string format, params object[] args)
        {
            Log(string.Format(format, args));
        }
        public static void LogWarning(string message)
        {
            Logic.Adapter.LogWarning(UtilDebug.GetCallerInfo(0) + " " + message);
        }
        public static void LogWarningFormat(string format, params object[] args)
        {
            LogWarning(string.Format(format, args));
        }
        public static void LogError(string message)
        {
            Logic.Adapter.LogError(UtilDebug.GetCallerInfo(0) + " " + message);
        }
        public static void LogErrorFormat(string format, params object[] args)
        {
            LogError(string.Format(format, args));
        }
        public static void LogCost(Action fun, string message)
        {
#if DEBUG
            var d0 = DateTime.UtcNow;
#endif
            fun();
#if DEBUG
            var d1 = DateTime.UtcNow;
            Logic.Adapter.Log(UtilDebug.GetCallerInfo(0) + " " + message + "耗时" + (d1 - d0).TotalSeconds + "秒");
#endif
        }
        public static void LogEachCost(Action fun, int count, string message)
        {
#if DEBUG
            var d0 = DateTime.UtcNow;
#endif
            int c = count;
            while (c > 0)
            {
                fun();
                c--;
            }
#if DEBUG
            var total = (DateTime.UtcNow - d0).TotalSeconds;
            Logic.Adapter.Log(UtilDebug.GetCallerInfo(0) + " " + message + "总耗时:" + total + "秒" + ",每次平均耗时:" + (total / count) + "秒");
#endif
        }

        public static void LogDebug<T>(T value)
        {
            Logic.Adapter.LogWarning(UtilDebug.GetCallerInfo(0) + " " + CachedType<T>.Info(value));
        }
    }
}