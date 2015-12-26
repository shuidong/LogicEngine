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
    /// <summary>
    /// 二维整型向量
    /// </summary>
    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public static Vector2i operator +(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.x + b.x, a.y + b.y);
        }
        public static Vector2i operator -(Vector2i a)
        {
            return new Vector2i(-a.x, -a.y);
        }
        public static Vector2i operator -(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.x - b.x, a.y - b.y);
        }

        public static bool operator ==(Vector2i lhs, Vector2i rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }
        public static bool operator !=(Vector2i lhs, Vector2i rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector2i)
            {
                return this == (Vector2i)obj;
            }
            return false;
        }
        public static Vector2i zero { get; private set; }
        public static Vector2i one { get; private set; }
        static Vector2i()
        {
            zero = new Vector2i();
            one = new Vector2i(1, 1);
        }
    }
}