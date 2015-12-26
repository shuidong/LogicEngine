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
    public static class UtilFile
    {
        public static byte[] LoadByte(string file_name, string suffix)
        {
            return Logic.Adapter.LoadByte(file_name, suffix);
        }

        public static string LoadText(string file_name, string suffix)
        {
            return Logic.Adapter.LoadText(file_name, suffix);
        }
    }
}