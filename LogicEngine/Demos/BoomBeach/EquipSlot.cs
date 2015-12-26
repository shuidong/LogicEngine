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
    public class EquipSlot : Part
    {
        public Equip equip { get; private set; }
        public Equip.Part part { get; internal set; }

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        public void Add(Equip equip)
        {
            this.equip = equip;
            Notify();
        }
        public void Remove()
        {
            equip = null;
            Notify();
        }
        public bool HasEquip()
        {
            return equip != null && !equip.IsRelease;
        }
        public int GetFightForce()
        {
            if (HasEquip())
            {
                return equip.GetFightForce();
            }
            return 0;
        }
    }
}