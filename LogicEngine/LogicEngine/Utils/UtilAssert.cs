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
    public static class UtilAssert
    {
        public static void IsTrue(bool condition, string message)
        {
            if (condition) return;
            Logic.Adapter.LogError(UtilDebug.GetCallerInfo(0) + " " + message);
        }

        public static void IsNull(object anObject, string message)
        {
            if (anObject == null) return;
            Logic.Adapter.LogError(UtilDebug.GetCallerInfo(0) + " " + anObject.GetType().Name + ":" + message);
        }

        public static void IsNotNull(object anObject, string message)
        {
            if (anObject != null) return;
            Logic.Adapter.LogError(UtilDebug.GetCallerInfo(0) + " " + message);
        }

        public static void Greater(int n1, int n2, string message)
        {
            if (n1 > n2) return;
            Logic.Adapter.LogError(UtilDebug.GetCallerInfo(0) + " " + n1 + "<=" + n2 + ":" + message);
        }
    }
}