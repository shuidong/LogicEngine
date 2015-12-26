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
    public abstract class StorySystem<TStoryType> : UPart
            where TStoryType : struct, IConvertible
    {
        PlistSet mSet;
        Dictionary<TStoryType, Actor> mActors = new Dictionary<TStoryType, Actor>();

        public void AddActor<TActor>(TStoryType type)where TActor : Actor,new()
        {
            mActors.Add(type, AddPart<TActor>());
        }
        public void Load(string file)
        {
            mSet = UtilCfg.LoadPlistSet(UtilFile.LoadText("Cfgs/" + file, "xml"));
            enableUpdate = true;
        }
        protected override void _Update(float sec)
        {
            if (mSet == null)
            {
                enableUpdate = false;
                return;
            }
        }
        public void Excute()
        {
        }
        public abstract class Actor : Part
        {
            protected abstract void OnExcute(Plist data);
        }
    }
}