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
    public static class UtilLambda
    {
        public static void Foreach(int end, Action<int> fun)
        {
            for (int index = 0; index < end; index++)
            {
                fun(index);
            }
        }
        public static void Foreach(int x_end, int y_end, Action<int, int> fun)
        {
            for (int index_x = 0; index_x < x_end; ++index_x)
            {
                for (int index_y = 0; index_y < y_end; ++index_y)
                {
                    fun(index_x, index_y);
                }
            }
        }
        public static void LoopRun(Action fun, int count)
        {
            for (int index = 0; index < count; index++)
            {
                fun();
            }
        }
    }
}