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
    public class CVaria : CPart<Varia>
    {
        string mPrefab;
        VVaria mVVaria;

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
            ReleaseBuildingPrefab();
        }
        protected override void _OnChange()
        {
            if (mPrefab == Part.cfg.prefab)
            {
                mVVaria.transform.position = Part.tile.Center.to3();
            }
            else
            {
                ReleaseBuildingPrefab();
                mPrefab = Part.cfg.prefab;
                mVVaria = UtilResource.LoadVisibleb<VVaria>("BoomBeach/Scenes/Varias/" + "GloveBox_S", Part.tile.Center.to3(), Quaternion.identity, "Varias", Part.cfg.prefab);
            }
        }
        void ReleaseBuildingPrefab()
        {
            if (mVVaria != null)
            {
                mVVaria.Release();
            }
        }
    }
}