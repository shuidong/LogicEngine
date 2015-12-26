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
    static class inConstants
    {
        public static readonly Type RequirePartAttribute;
        public static readonly Type RequirePartInParentAttribute;
        public static readonly Type CmdPostAttribute;
        public static readonly List<Type> ForClear = new List<Type>();
        public static readonly List<bool> ForBool = new List<bool>();
        static inConstants()
        {
            RequirePartAttribute = typeof(RequirePartAttribute);
            RequirePartInParentAttribute = typeof(RequirePartInParentAttribute);
            CmdPostAttribute = typeof(CmdPostAttribute);
        }
    }
}