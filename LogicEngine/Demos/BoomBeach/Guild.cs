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
    public class Guild : Part<DycGuild>
    {
        public string Name { get; private set; }

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _Sync(DycGuild dyc)
        {
        }
        protected override void _Save(DycGuild dyc)
        {
        }
    }

    public class DycGuild : Dyc
    {
        /// <summary>
        /// Guild's name
        /// </summary>
        public string d0;
        /// <summary>
        /// Guild's level
        /// </summary>
        public int d1;

        protected override bool _TryRevise()
        {
            return true;
        }
    }
}