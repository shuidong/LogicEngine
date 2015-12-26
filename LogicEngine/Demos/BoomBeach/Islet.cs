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
    public class Islet : Module<Islet, DycIslet>
    {
        public CfgIslet cfg { get; private set; }
        public TileMap<Mask> tilemap { get; private set; }
        public PathFinder<Mask> finder { get; private set; }
        public EntityMgr<Building, DycBuilding> buildings { get; private set; }
        public EntityMgr<Varia, DycVaria> varias { get; private set; }
        public EntityMgr<Cooly> coolies { get; private set; }

        protected override void _Awake()
        {
            buildings = AddPart<EntityMgr<Building, DycBuilding>>();
            varias = AddPart<EntityMgr<Varia, DycVaria>>();
            coolies = AddPart<EntityMgr<Cooly>>();
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycIslet dyc)
        {
            cfg = CfgMgr.Islets.Get(dyc.d0);
            UtilAssert.IsNotNull(cfg, "CfgIslet{" + dyc.d0 + "} is null");
            tilemap = new TileMap<Mask>(cfg.map_size, cfg.tile_size, cfg.center);
            finder = new PathFinder<Mask>(tilemap);
            buildings.Sync(dyc.d1);
            varias.Sync(dyc.d2);
        }
        protected override void _Save(DycIslet dyc)
        {
            buildings.SaveTo(ref dyc.d1);
            varias.SaveTo(ref dyc.d2);
        }
        public Varia GetRandomVaria()
        {
            if (varias.Count == 0)
            {
                return null;
            }
            return UtilRandom.Select(varias.FindAll(it => true));
        }

        public enum Mask
        {
            None = 0,
            Blocked = 1 << 0
        }
    }

    public class CfgIslet : Cfg
    {
        public readonly string tid_name;
        public readonly string tid_desc;
        public readonly Vector2i map_size;
        public readonly Vector2f tile_size;
        public readonly Vector2f center;
        public readonly string prefab;
    }
    public enum IsletObjectType
    { 
        Building,
        Varia
    }
    public class CfgIsletObject : Cfg
    {
        public readonly string object_id;
        public readonly IsletObjectType object_type;
        public readonly int level;
        public readonly Vector2i tile;
    }

    public class DycIslet : Dyc
    {
        /// <summary>
        /// Cfg's id
        /// </summary>
        public string d0;
        public List<DycBuilding> d1 = new List<DycBuilding>();
        public List<DycVaria> d2 = new List<DycVaria>();

        protected override bool _TryRevise()
        {
            if (HasSthWrong(d1))
            {
                return false;
            }
            if (HasSthWrong(d2))
            {
                return false;
            }
            return true;
        }
    }
}