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
    public class Cellular1
    {
        public bool[][] cells { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        public Cellular1(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new bool[width][];
            for (int i = 0; i < width; i++)
            {
                cells[i] = new bool[height];
            }
        }

        public void Run(int rule_code)
        {
            if (rule_code >= 0 && rule_code < 256)
            {
                Reset();
                for (int j = height - 2; j >= 0; j--)
                {
                    for (int i = 1; i < width - 1; i++)
                    {
                        var jpre = j + 1;
                        cells[i][j] = GetValue(rule_code, GetIndex(cells[i - 1][jpre], cells[i][jpre], cells[i + 1][jpre]));
                    }
                }
            }
        }
        void Reset()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i][j] = false;
                }
            }
            cells[width / 2][height - 1] = true;
        }
        int GetIndex(bool a, bool b, bool c)
        {
            return (a ? 4 : 0) + (b ? 2 : 0) + (c ? 1 : 0);
        }
        bool GetValue(int rule_code, int index)
        {
            return (rule_code & ((int)UtilMath.Pow(2, index))) > 0;
        }
    }
}