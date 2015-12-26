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
using LogicEngine.AI;

namespace Demos.BoomBeach
{
    public class Cooly : Islet.Part
    {
        //public Vector2i tile;
        public Vector2f position
        {
            get; private set;
        }
        public bool IsBattle { get; set; }
        protected override void _Awake()
        {
            position = Module.tilemap.ToWorld(new Vector2i(43, 10) + (UtilRandom.onUnitCircle * 5).RoundToVector2i(), Vector2f.half);
            AddPart<AiSCooly.Spirit>();
        }
        protected override void _Release()
        {
        }
        public Varia GetRandomVaria()
        {
            return Module.GetRandomVaria();
        }
        public List<Vector2f> GetPathToRandomVaria()
        {
            var varia = GetRandomVaria();
            var vtile = Module.tilemap.ToTile(varia.tile.Center);
            return Module.finder.Find(Module.tilemap.ToTile(position), vtile, Islet.Mask.Blocked, false).Map(it => Module.tilemap.ToWorld(it, Vector2f.half));
        }
        public List<Vector2f> GetPathToBase()
        {
            var varia = Module.buildings.Find(it => it.cfg.id == "CommandHouse");
            var vtile = Module.tilemap.ToTile(varia.tile.Center);
            return Module.finder.Find(Module.tilemap.ToTile(position), vtile, Islet.Mask.Blocked, false).Map(it => Module.tilemap.ToWorld(it, Vector2f.half));
        }
        public void LockTarget(Cooly c)
        {
        }
        public void SetPosition(Vector2f pos)
        {
            position = pos;
            Notify();
        }
    }
}