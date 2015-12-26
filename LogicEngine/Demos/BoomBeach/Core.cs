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
    public static class SpecialItemID
    {
        public static readonly long Gold = 0;
        public static readonly long Wood = 1;
        public static readonly long Cloth = 2;
        public static readonly long Metal = 3;
        public static readonly long Diamond = 4;
    }

    public class Core : Module<Core, DycCore, Core.Message>
    {
        public string name { get; private set; }
        public int level { get; private set; }
        public EntityMgr<Item, DycItem> items { get; private set; }
        public Item gold { get; private set; }
        public Item wood { get; private set; }
        public Item cloth { get; private set; }
        public Item metal { get; private set; }
        public Item diamond { get; private set; }
        public EntityMgr<Equip, DycEquip> equips { get; private set; }
        public EntityMgr<Hero, DycHero> heros { get; private set; }

        protected override void _Awake()
        {
            //name = AddPart<Name>();
            //level = AddPart<Level>();
            items = AddPart<EntityMgr<Item, DycItem>>();
            equips = AddPart<EntityMgr<Equip, DycEquip>>();
            heros = AddPart<EntityMgr<Hero, DycHero>>();
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycCore dyc)
        {
            //name.SetName(dyc.d0);
            //UtilLog.LogWarning(name.name);
            //level.SetLevel(dyc.d1);

            items.Sync(dyc.d2);
            gold = items.Find(SpecialItemID.Gold);
            wood = items.Find(SpecialItemID.Wood);
            cloth = items.Find(SpecialItemID.Cloth);
            diamond = items.Find(SpecialItemID.Diamond);
            metal = items.Find(SpecialItemID.Metal);

            equips.Sync(dyc.d3);
            heros.Sync(dyc.d4);
        }

        protected override void _Save(DycCore dyc)
        {
            //dyc.d0 = name.name;
            //dyc.d1 = level.lv;
            items.SaveTo(ref dyc.d2);
            equips.SaveTo(ref dyc.d3);
            heros.SaveTo(ref dyc.d4);
        }

        public enum Message : int
        {
            NameChange,
        }
    }
    public class DycCore : Dyc
    {
        /// <summary>
        /// player's name
        /// </summary>
        public string d0;
        /// <summary>
        /// player's lv
        /// </summary>
        public int d1;
        /// <summary>
        /// player's items
        /// </summary>
        public List<DycItem> d2 = new List<DycItem>();
        public List<DycEquip> d3 = new List<DycEquip>();
        public List<DycHero> d4 = new List<DycHero>();

        protected override bool _TryRevise()
        {
            if (HasSthWrong(d2))
            {
                return false;
            }
            return true;
        }
    }
}