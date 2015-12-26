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

namespace Demos.BoomBeach.Client.Parts
{
    public class VIslet : Visible
    {
        protected override void _Awake()
        {
            foreach (var it in GetComponentsInChildren<Renderer>())
            {
            }
        }
    }
}