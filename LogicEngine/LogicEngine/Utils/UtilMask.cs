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
    public static class UtilMask
    {
        public static long Empty { get { return 0; } }

        public static long Full { get { return ~0; } }

        public static long Add(long mask_a, long mask_b)
        {
            return mask_a | mask_b;
        }

        public static long Sub(long mask_a, long mask_b)
        {
            return mask_a & (~mask_b);
        }

        public static bool Test(long mask_a, long mask_b)
        {
            return (mask_a & mask_b) != 0;
        }
    }
}