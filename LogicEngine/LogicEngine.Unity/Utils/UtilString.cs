//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Text;
using UnityEngine;

namespace LogicEngine.Unity
{
    public static  class UtilString
    {
        public static Vector2 ToVector2(string pack)
        {
            string[] splits = pack.Split(',');
            Vector2 ret;
            ret.x = Convert.ToSingle(splits[0]);
            ret.y = Convert.ToSingle(splits[1]);
            return ret;
        }
        public static Vector3f ToVector3f(string pack)
        {
            string[] splits = pack.Split(',');
             Vector3f ret;
            ret.x = Convert.ToSingle(splits[0]);
            ret.y = Convert.ToSingle(splits[1]);
            ret.z = Convert.ToSingle(splits[2]);
            return ret;
        }
    }
}
