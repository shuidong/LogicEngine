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
    static class UtilEnumerator
    {
        public static void Run(IEnumerator e)
        {
            while (true)
            {
                if (e.MoveNext())
                {
                    IEnumerator se = e.Current as IEnumerator;
                    if (se != null)
                    {
                        Run(se);
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}