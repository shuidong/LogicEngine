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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequirePartAttribute : Attribute
    {
        public Type RequirePart { get; private set; }

        public RequirePartAttribute(Type required_part)
        {
            RequirePart = required_part;
        }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequirePartInParentAttribute : Attribute
    {
        public Type RequirePart { get; private set; }

        public RequirePartInParentAttribute(Type required_part)
        {
            RequirePart = required_part;
        }
    }
}