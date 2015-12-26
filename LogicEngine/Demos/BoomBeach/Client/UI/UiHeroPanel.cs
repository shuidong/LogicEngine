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
    [AddComponentMenu("DemoUI/ReferHeroPanel")]
    public class ReferHeroPanel : Refer
    {
        public Image[] stars;
        public Image heroType;
        public ReferSlotEquip equip0;
        public ReferSlotEquip equip1;
        public ReferSlotEquip equip2;
        public ReferSlotEquip equip3;
        public ReferSlotEquip equip4;
        public ReferSlotEquip equip5;

        public Text_i18n Name;
        public Text_i18n Desc;

        protected override void _Awake()
        {
        }
        void Update()
        {
            UtilDevelop.LogValue("test", transform.position.y);
        }
    }
    class UiHeroPanel : Ui<ReferHeroPanel>
    {
        public UiSlotEquip equip0 { get; private set; }
        public UiSlotEquip equip1 { get; private set; }
        public UiSlotEquip equip2 { get; private set; }
        public UiSlotEquip equip3 { get; private set; }
        public UiSlotEquip equip4 { get; private set; }
        public UiSlotEquip equip5 { get; private set; }

        protected override void _Init()
        {
            equip0 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip0);
            equip1 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip1);
            equip2 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip2);
            equip3 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip3);
            equip4 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip4);
            equip5 = UtilUi.CreateUi<UiSlotEquip, ReferSlotEquip>(Refer.equip5);
        }

        public void SetPanel(string name, string desc)
        {
            Refer.Name.SetPhrase(name);
            Refer.Desc.SetPhrase(desc);
        }
    }
}