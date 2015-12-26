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
    /// 坐标系(x,y)，0度角为Vector2f.up
    /// 标准角度范围[0, 360)
    /// </summary>
    public static class UtilGeometry
    {
        const float Deg2Rad = 0.0174533f;
        const float Epsilon = 1.4013e-045f;

        public static Vector2f Rotate(Vector2f v, float angle)
        {
            angle = -angle;

            float cos = (float)Math.Cos(angle * Deg2Rad);
            float sin = (float)Math.Sin(angle * Deg2Rad);

            float x = v.x * cos - v.y * sin;
            float z = v.x * sin + v.y * cos;

            return new Vector2f(x, z);
        }
        public static float GetAngle(Vector2f v)
        {
            return GetAngle(Vector2f.up, v);
        }
        public static float GetAngle(Vector2f b, Vector2f v)
        {
            b.Normalize();
            v.Normalize();
            float angle = (float)Math.Acos((double)Vector2f.Dot(b, v)) * UtilMath.Rad2Deg;
            var c = Vector3f.Cross(b, v);
            return c.z <= 0 ? angle : 360 - angle;
        }

        public static Vector2f GetNormalPoint(Vector2f point, Vector2f line_a, Vector2f line_b)
        {
            var ap = point - line_a;
            var ab = (line_b - line_a).normalized;
            return ab * (Vector2f.Dot(ap, ab)) + line_a;
        }

        public static void TestCase01()
        {
            LogRotate(Vector2f.up, 0f);
            LogRotate(Vector2f.up, 30f);
            LogRotate(Vector2f.up, 45f);
            LogRotate(Vector2f.up, 60f);

            LogRotate(Vector2f.up, 90f);
            LogRotate(Vector2f.up, 120f);
            LogRotate(Vector2f.up, 135f);
            LogRotate(Vector2f.up, 150f);

            LogRotate(Vector2f.up, 180f);
            LogRotate(Vector2f.up, 210f);
            LogRotate(Vector2f.up, 225f);
            LogRotate(Vector2f.up, 240f);

            LogRotate(Vector2f.up, 270f);
            LogRotate(Vector2f.up, 300f);
            LogRotate(Vector2f.up, 315f);
            LogRotate(Vector2f.up, 330f);

            LogRotate(Vector2f.up, 360f);
        }
        public static void TestCase02()
        {
            float count = 36;
            for (int i = 0; i <= count; i++)
            {
                LogGetAngle(i * 360 / count);
            }
            LogGetAngle(359.9f);
            LogGetAngle(359.99f);
            LogGetAngle(359.999f);
            LogGetAngle(359.9999f);
            LogGetAngle(359.99999f);
        }
        static void LogRotate(Vector2f v, float angle)
        {
            UtilLog.LogWarning(v + "rotation " + angle + " -> " + Rotate(v, angle));
        }
        static void LogGetAngle(float angle)
        {
            UtilLog.LogWarning(angle + " -> " + GetAngle(Rotate(Vector2f.up, angle)));
        }
    }
}