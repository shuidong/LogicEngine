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

namespace Demos.BoomBeach.Client
{
    static class UiMgr
    {
        public static UI.UiHeroPanel UiHeroPanel { get; private set; }

        static UiMgr()
        {
            UiHeroPanel = UtilUi.CreateUi<UI.UiHeroPanel>("ReferHeroPanel");
        }
    }
}