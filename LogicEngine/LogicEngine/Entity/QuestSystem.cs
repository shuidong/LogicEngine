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

namespace LogicEngine
{
    public class QuestSystem<TTrigger> : Part
            where TTrigger : struct, IConvertible
    {
        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        public void AddTrigger<T>(TTrigger type)
        {
        }

        public abstract class Trigger : Part
        {
        }
    }
}