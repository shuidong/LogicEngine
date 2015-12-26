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
    public class Varia : Islet.Part<DycVaria>
    {
        public CfgVaria cfg { get; private set; }
        public Tile tile { get; private set; }
        
        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycVaria dyc)
        {
            cfg = CfgMgr.Varias.Get(dyc.d0);
            UtilAssert.IsNotNull(cfg, "CfgVaria{" + dyc.d0 + "} is null");
            tile = new Tile(cfg.tile_size, Module.tilemap.TileSize, Module.tilemap.ToWorld(new Vector2i(dyc.d2x, dyc.d2y), Vector2f.half));
        }
        protected override void _Save(DycVaria dyc)
        {
        }
    }
    public class CfgVaria : Cfg
    {
        public readonly string tid_name;
        public readonly string tid_desc;
        public readonly Vector2i tile_size;
        public readonly string prefab;
    }
    public class DycVaria : Dyc
    {
        /// <summary>
        /// CfgBuilding's id
        /// </summary>
        public string d0;
        /// <summary>
        /// Building's tile
        /// </summary>
        public int d2x;
        public int d2y;

        protected override bool _TryRevise()
        {
            return true;
        }
    }
}