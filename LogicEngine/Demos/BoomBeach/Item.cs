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
using LogicEngine;

namespace Demos.BoomBeach
{
    public class Item : Part<DycItem>
    {
        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycItem dyc)
        {
        }
        protected override void _Save(DycItem dyc)
        {
        }
    }
    public class DycItem : Dyc
    {
        protected override bool _TryRevise()
        {
            return true;
        }
    }
}