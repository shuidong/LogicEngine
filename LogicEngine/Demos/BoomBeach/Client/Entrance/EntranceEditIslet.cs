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
using LogicEngine.VirtualNet;
using UnityEngine;

using Demos.BoomBeach.Server;

namespace Demos.BoomBeach.Client
{
    public class EntranceEditIslet : LogicEngine.Unity.Entrance
    {
        Islet mIslet;

        protected override void _Init()
        {
            mIslet = Logic.AddChild().AddPart<Islet>();
            var dyc = UtilRandom.Dyc<DycIslet>();
            dyc.d0 = "islet00";
            mIslet.Sync(dyc);

            var sceneMgr = mIslet.entity.AddPart<SceneMgr>();
            sceneMgr.Register<Scenes.SceneEditIslet>("EditIslet", mIslet);
            sceneMgr.Switch("EditIslet");
        }
    }
}