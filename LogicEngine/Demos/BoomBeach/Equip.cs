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
    public class Equip : Part<DycEquip>
    {
        public Part part { get; private set; }

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycEquip dyc)
        {
            //UtilLog.LogWarning("equip");
        }
        protected override void _Save(DycEquip dyc)
        {
        }
        public int GetFightForce()
        {
            return 1000;
        }

        public enum Part
        {
            Hat,
            Armor,
            Shoe,
            HeadCollar,
            Necklace,
            Bracelet,
        }
    }
    public class DycEquip : Dyc
    {
        protected override bool _TryRevise()
        {
            return true;
        }
    }
}