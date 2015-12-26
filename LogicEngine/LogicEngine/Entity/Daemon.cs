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
using System.Reflection;

namespace LogicEngine
{
    public class Daemon : Part
    {
        Dictionary<string, Entity.IPart> mCenters = new Dictionary<string, Entity.IPart>();

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }

        public void RunCenter(string class_fullname)
        {
            if (mCenters.ContainsKey(class_fullname))
            {
                UtilLog.LogWarning("已经启动了 " + class_fullname);
                return;
            }
            Type type = System.Type.GetType(class_fullname, false);
            if (type == null)
            {
            }
            else
            {
                var part = entity.AddChild().AddPart(type);
                mCenters.Add(class_fullname, part);
            }
        }
        public void KillCenter(string class_fullname)
        {
            Entity.IPart part;
            if (mCenters.TryGetValue(class_fullname, out part))
            {
                entity.RemovePart(part.GetType());
            }
        }
    }
}