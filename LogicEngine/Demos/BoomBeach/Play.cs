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
    public class Play : Play<Play, DycRole, Play.Message, Play.RpcMessage>
    {
        public Core core { get; private set; }
        public Mainbase mainbase { get; private set; }
        public Fightbase fightbase { get; private set; }
        
        protected override void _Awake()
        {
            core = AddPart<Core>();
            mainbase = AddChild().AddPart<Mainbase>();
            fightbase = AddChild().AddPart<Fightbase>();
        }
        protected override void _Release()
        {
        }
        protected override void _Load(DycRole dyc)
        {
            core.Sync(dyc.d0);
            mainbase.Sync(dyc.d1);
        }
        protected override void _Save(DycRole dyc)
        {
            core.Save();
            mainbase.Save();
        }
        public enum Message : int
        {
        }
        public enum RpcMessage : long
        {
        }
    }

    public class DycRole : Dyc
    {
        public DycCore d0 = new DycCore();
        public DycMainbase d1 = new DycMainbase();

        protected override bool _TryRevise()
        {
            return true;
        }
    }
}