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
using LogicEngine.Unity;

namespace Demos.BoomBeach.Client
{
    class CSlotEquip : CPart<EquipSlot>
    {
        UI.UiSlotEquip uiSlot;

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _OnChange()
        {
            if (uiSlot == null) return;
            if (Part.HasEquip())
            {
                uiSlot.SetSlot("neck", 5);
            }
            else
            {
                uiSlot.SetSlot("neck", 3);
            }
        }

        public void SetUiSlot(UI.UiSlotEquip uiSlot)
        {
            this.uiSlot = uiSlot;
            uiSlot.Show();
        }
    }
}
