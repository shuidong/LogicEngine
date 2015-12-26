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
    public static class ExtendInt
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(this int number)
        {
            return number % 2 == 1;
        }
        public static bool InRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        public static float Abs(this int value)
        {
            return value < 0 ? -value : value;
        }
    }
}