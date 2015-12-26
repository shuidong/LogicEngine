
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
#if DEBUG
using System.Diagnostics;
#endif

namespace LogicEngine
{
    internal static class UtilDebug
    {
        public static string GetCallerInfo(int index)
        {
#if DEBUG
            StackFrame caller_frame = new StackTrace(true).GetFrame(index + 2);
            return "[" + caller_frame.GetMethod().ReflectedType.Name + ":" + caller_frame.GetFileLineNumber() + "]";
#else
        return string.Empty;
#endif
        }
    }
}