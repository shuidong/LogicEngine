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
    public class PathFinder<T> where T : struct, IConvertible
    {
        AStar<T> aStar;

        public PathFinder(TileMap<T> map)
        {
            aStar = new AStar<T>(map);
        }

        public List<Vector2i> Find(Vector2i start, Vector2i end, T block, bool ignore_corner)
        {
            aStar.SetBlock(block);
            return aStar.Find(start, end, ignore_corner);
        }
    }
}