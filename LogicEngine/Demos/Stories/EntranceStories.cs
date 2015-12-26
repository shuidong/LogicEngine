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
using UnityEngine;
using LogicEngine;
using LogicEngine.Unity;

namespace Demos.Stories
{
    public class EntranceStories : Entrance
    {
        protected override void _Init()
        {
            var story = Logic.AddChild().AddPart<StorySystem>();
            story.Load("Stories/tutorial-baseopration");
        }
    }
}