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
    class CHeroPanel : CPart<Hero>
    {
        protected override void _Awake()
        {
            Part.slot0.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip0);
            Part.slot1.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip1);
            Part.slot2.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip2);
            Part.slot3.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip3);
            Part.slot4.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip4);
            Part.slot5.entity.AddPart<CSlotEquip>().SetUiSlot(UiMgr.UiHeroPanel.equip5);
            UiMgr.UiHeroPanel.Show();
        }
        protected override void _Release()
        {
            UiMgr.UiHeroPanel.Hide();
        }
        protected override void _OnChange()
        {
            UiMgr.UiHeroPanel.SetPanel(Part.tidName, Part.tidDesc);
        }
    }
}