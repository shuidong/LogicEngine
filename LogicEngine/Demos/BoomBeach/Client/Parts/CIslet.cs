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
    public class CIslet : CPart<Islet>
    {
        string mPrefab;
        VIslet mVIslet;

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
            ReleaseIsletPrefab();
        }
        protected override void _OnChange()
        {
            if (mPrefab == Part.cfg.prefab)
            {
            }
            else
            {
                ReleaseIsletPrefab();
                mPrefab = Part.cfg.prefab;
                mVIslet = UtilResource.LoadVisibleb<VIslet>("BoomBeach/Scenes/Islets/" + mPrefab, Part.cfg.center.to3(), Quaternion.identity, "Islet", Part.cfg.prefab);
            }
        }
        void ReleaseIsletPrefab()
        {
            if (mVIslet != null)
            {
                mVIslet.Release();
            }
        }
    }
}