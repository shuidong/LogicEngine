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
    /// 三维整型向量
    /// </summary>
    public struct Vector3i
    {
        public int x;
        public int y;
        public int z;

        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
        }

        public static Vector3i operator +(Vector3i a, Vector3i b)
        {
            return new Vector3i(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3i operator -(Vector3i a)
        {
            return new Vector3i(-a.x, -a.y, -a.z);
        }
        public static Vector3i operator -(Vector3i a, Vector3i b)
        {
            return new Vector3i(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static bool operator ==(Vector3i lhs, Vector3i rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }
        public static bool operator !=(Vector3i lhs, Vector3i rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector3i)
            {
                return this == (Vector3i)obj;
            }
            return false;
        }
    }
}