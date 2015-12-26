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
    public static class ExtendFloat
    {
        public static bool IsEven(this float number)
        {
            return number % 2 == 0;
        }

        public static bool IsOdd(this float number)
        {
            return number % 2 == 1;
        }
        public static bool InRange(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
        public static float Abs(this float value)
        {
            return value < 0 ? -value : value;
        }
        public static float Square(this float number)
        {
            return number * number;
        }
        public static float Sqrt(this float x)
        {
            return (float)Math.Sqrt(x);
        }
        public static float Acos(this float x)
        {
            return (float)Math.Acos(x);
        }
        public static float Cos(this float radius)
        {
            return (float)Math.Cos(radius);
        }
        public static float Sin(this float radius)
        {
            return (float)Math.Sin(radius);
        }
        public static bool Approximately(this float x, float y)
        {
            return (x - y).Abs() < UtilMath.Epsilon;
        }
    }
}