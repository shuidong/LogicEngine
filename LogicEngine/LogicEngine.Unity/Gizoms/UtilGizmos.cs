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

namespace LogicEngine.Unity
{
    public static class UtilGizmos
    {
        public static void DrawTile(Vector2i size, Vector2f tilesize, Vector2f origin, Color color)
        {
            Gizmos.color = color;

            float y0 = origin.y;
            float y1 = y0 + tilesize.y * size.y;
            for (int i = 0; i <= size.x; i++)
            {
                float x = origin.x + tilesize.x * i;
                Gizmos.DrawLine(new Vector3(x, 0, y0), new Vector3(x, 0, y1));
            }
            float x0 = origin.x;
            float x1 = x0 + tilesize.x * size.x;
            for (int i = 0; i <= size.y; i++)
            {
                float y = origin.y + tilesize.y * i;
                Gizmos.DrawLine(new Vector3(x0, 0, y), new Vector3(x1, 0, y));
            }
        }
    }
}