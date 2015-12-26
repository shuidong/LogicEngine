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

namespace Demos.Stories
{
    public enum StoryType
    {
        /// <summary>
        /// 旁白
        /// </summary>
        Aside,
    }
    public enum StoryMessage
    {
        Confirm
    }

    public class StorySystem : StorySystem<StoryType>
    {
        protected override void _Awake()
        {
            AddActor<Aside>(StoryType.Aside);
        }
        protected override void _Release()
        {
        }
    }

    class Aside : StorySystem.Actor
    {
        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void OnExcute(Plist data)
        {
            UtilLog.LogWarning(data.GetString("dialogue", "未配置旁白"));
        }
    }
}