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
    public class Mainbase : Module<Mainbase, DycMainbase, Mainbase.Message>
    {
        public Islet islet { get; private set; }

        protected override void _Awake()
        {
            islet = AddPart<Islet>();
            islet.buildings.Attach(OnAddBuilding);
            islet.varias.Attach(OnAddVaria);
        }
        protected override void _Release()
        {
            islet.buildings.Detach(OnAddBuilding);
            islet.varias.Detach(OnAddVaria);
        }
        protected override void _Sync(DycMainbase dyc)
        {
            islet.Sync(dyc.d0);

            islet.coolies.Add();
            //islet.coolies.Add();
            //islet.coolies.Add();
        }
        protected override void _Save(DycMainbase dyc)
        {
            dyc.d0 = islet.Save();
        }
        public enum Message : int
        {
        }
        void OnAddBuilding(Building building)
        {
        }
        void OnAddVaria(Varia varia)
        {
        }
    }
    public class DycMainbase : Dyc
    {
        public DycIslet d0 = new DycIslet();

        protected override bool _TryRevise()
        {
            return true;
        }
    }
}