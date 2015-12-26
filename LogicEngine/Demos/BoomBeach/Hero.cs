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
    public enum HeroType
    {
        INT,
        AGI,
        STR
    }
    public class Hero : Part<DycHero>
    {
        CfgHero cfg;
        public EquipSlot slot0 { get; private set; }
        public EquipSlot slot1 { get; private set; }
        public EquipSlot slot2 { get; private set; }
        public EquipSlot slot3 { get; private set; }
        public EquipSlot slot4 { get; private set; }
        public EquipSlot slot5 { get; private set; }
        public string tidName { get; private set; }
        public string tidDesc { get; private set; }
        public int LV { get; private set; }
        public int INT { get; private set; }
        public int AGI { get; private set; }
        public int STR { get; private set; }

        protected override void _Awake()
        {
            slot0 = AddChild().AddPart<EquipSlot>();
            slot1 = AddChild().AddPart<EquipSlot>();
            slot2 = AddChild().AddPart<EquipSlot>();
            slot3 = AddChild().AddPart<EquipSlot>();
            slot4 = AddChild().AddPart<EquipSlot>();
            slot5 = AddChild().AddPart<EquipSlot>();
            slot0.part = Equip.Part.Hat;
            slot1.part = Equip.Part.Armor;
            slot2.part = Equip.Part.Shoe;
            slot3.part = Equip.Part.HeadCollar;
            slot4.part = Equip.Part.Necklace;
            slot5.part = Equip.Part.Bracelet;
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycHero dyc)
        {
            cfg = CfgMgr.Heros.Get(dyc.d0);
            LV = dyc.d1;

            tidName = cfg.tid_name;
            tidDesc = cfg.tid_desc;

            //UtilLog.LogWarning(cfg.tid_desc);
        }
        protected override void _Save(DycHero dyc)
        {
        }
        public int GetFightForce()
        {
            int force = 0;
            force += slot0.GetFightForce();
            force += slot1.GetFightForce();
            force += slot2.GetFightForce();
            force += slot3.GetFightForce();
            force += slot4.GetFightForce();
            force += slot5.GetFightForce();
            return force;
        }
    }

    [Cfg.Meta()]
    public class CfgHero : Cfg
    {
        public readonly string tid_name;
        public readonly string tid_desc;
        public readonly HeroType hero_type;
        [Cfg.Range(0, 1000)]
        public readonly int HP;
    }
    public class DycHero : Dyc
    {
        /// <summary>
        /// cfg's id
        /// </summary>
        public string d0;
        /// <summary>
        /// lv
        /// </summary>
        public int d1;

        protected override bool _TryRevise()
        {
            return true;
        }
    }
}