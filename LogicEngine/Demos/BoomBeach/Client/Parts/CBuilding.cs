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
    public class CBuilding : CPart<Building>
    {
        string mPrefab;
        VBuilding mVBuilding;

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
            ReleaseBuildingPrefab();
        }
        protected override void _OnChange()
        {
            if (mPrefab == Part.cfg.prefab[0])
            {
                mVBuilding.transform.position = Part.tile.Center.to3();
            }
            else
            {
                ReleaseBuildingPrefab();
                mPrefab = Part.cfg.prefab[0];
                //mVBuilding = UtilResource.LoadVisibleb<VBuilding>("BoomBeach/Buildings/" + mPrefab, Part.tile.Center.to3(), Quaternion.identity, "Buildings", Part.cfg.name);
                mVBuilding = UtilResource.LoadVisibleb<VBuilding>("BoomBeach/Buildings/Exchange-lv6", Part.tile.Center.to3(), Quaternion.identity, "Buildings", Part.cfg.name);
            }
        }
        void ReleaseBuildingPrefab()
        {
            if (mVBuilding != null)
            {
                mVBuilding.Release();
            }
        }
    }
}