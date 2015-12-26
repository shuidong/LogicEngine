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

namespace LogicEngine.Physics2D
{
    public class Path
    {
        public float Radius { get; set; }
        List<Vector2f> path = new List<Vector2f>();

        public int Count { get { return path.Count; } }
        public Vector2f this[int index] { get { return path[index]; } set { path[index] = value; } }
        public void Add(Vector2f point)
        {
            path.Add(point);
        }
        public Vector2f First
        {
            get
            {
                return path[0];
            }
        }
        public Vector2f Last
        {
            get
            {
                return path[path.Count - 1];
            }
        }
    }
}