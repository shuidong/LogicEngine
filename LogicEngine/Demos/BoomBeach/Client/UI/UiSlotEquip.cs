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
using UnityEngine;
using UnityEngine.UI;

namespace Demos.BoomBeach.Client.UI
{
    [AddComponentMenu("DemoUI/ReferSlotEquip")]
    public class ReferSlotEquip : Refer
    {
        public Image frame;
        public Image icon;
        public Image[] stars;

        protected override void _Awake()
        {
        }
    }
    class UiSlotEquip : Ui<ReferSlotEquip>
    {
        protected override void _Init()
        {
        }
        public void SetSlot(string icon, int star)
        {
            Refer.stars.SetStar(star);
        }
    }
}