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

namespace Demos.BoomBeach
{
    public static class CfgMgr
    {
        public static CfgSet<CfgHero> Heros { get; private set; }
        public static CfgSet<CfgIslet> Islets { get; private set; }
        public static CfgSet<CfgBuilding> Buildings { get; private set; }
        public static CfgSet<CfgVaria> Varias { get; private set; }
        public static CfgSet<CfgIsletObject> MainbaseObjects { get; private set; }

        static CfgMgr()
        {
            Heros = Load<CfgHero>("Heros");
            Islets = Load<CfgIslet>("Islets");
            Buildings = Load<CfgBuilding>("Buildings");
            Varias = Load<CfgVaria>("Varias");
            MainbaseObjects = Load<CfgIsletObject>("IsletObjects/Base/Mainbase");
        }

        public static CfgSet<T> Load<T>(string cfg_file) where T : Cfg, new()
        {
            return UtilCfg.LoadCfgSet<T>(UtilFile.LoadText("Cfgs/" + cfg_file, "xml"));
        }
    }
}