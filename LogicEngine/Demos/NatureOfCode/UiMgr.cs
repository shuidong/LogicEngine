//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections.Generic;
using LogicEngine.Unity;
using UnityEngine;

namespace Demos.NatureOfCode
{
    public static class UiMgr
    {
        public static LogicEngine.Unity.Toolkit.UiSketch UiSketch { get; private set; }

        static UiMgr()
        {
            UiSketch = UtilUi.CreateUi<LogicEngine.Unity.Toolkit.UiSketch>(new UnityEngine.GameObject("UiSketch", typeof(RectTransform)));
            UiSketch.SetLayer(UiLayer.Normal);
            UiSketch.Clear(Color.black);
        }
    }
}