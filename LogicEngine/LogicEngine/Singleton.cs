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
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        static T singleton;

        public static T Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new T();
                    singleton._Init();
                }
                return singleton;
            }
        }

        protected abstract void _Init();
        protected Singleton() { }
    }
}
