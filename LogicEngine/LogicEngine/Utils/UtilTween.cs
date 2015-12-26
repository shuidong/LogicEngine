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
    public enum TweenStyle
    {
        Linear,
        EaseIn, EaseOut, EaseInOut, BounceIn, BounceOut,
        QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn,
        ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn,
        CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn,
        QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn,
        QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn,
        CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn,
        SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn,
        ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn,
        BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn,
        BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn
    }

    public static class UtilTween
    {
        public static Vector2f[] GetPathControlPoints(Vector2f[] path)
        {
            Vector2f[] suppliedPath;
            Vector2f[] vector3s;

            suppliedPath = path;

            int offset = 2;
            vector3s = new Vector2f[suppliedPath.Length + offset];
            Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);

            vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
            vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);

            if (vector3s[1] == vector3s[vector3s.Length - 2])
            {
                Vector2f[] tmpLoopSpline = new Vector2f[vector3s.Length];
                Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
                tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
                tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
                vector3s = new Vector2f[tmpLoopSpline.Length];
                Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
            }

            return vector3s;
        }
        public static Vector2f Interp(Vector2f[] c_points, float t)
        {
            int numSections = c_points.Length - 3;
            int currPt = UtilMath.Min((int)Math.Floor(t * (float)numSections), numSections - 1);
            float u = t * (float)numSections - (float)currPt;

            Vector2f a = c_points[currPt];
            Vector2f b = c_points[currPt + 1];
            Vector2f c = c_points[currPt + 2];
            Vector2f d = c_points[currPt + 3];

            return .5f * (
                (-a + 3f * b - 3f * c + d) * (u * u * u)
                + (2f * a - 5f * b + 4f * c - d) * (u * u)
                + (-a + c) * u
                + 2f * b
            );
        }
        public static Vector3f[] GetPathControlPoints(Vector3f[] path)
        {
            Vector3f[] suppliedPath;
            Vector3f[] vector3s;

            suppliedPath = path;

            int offset = 2;
            vector3s = new Vector3f[suppliedPath.Length + offset];
            Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);

            vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
            vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);

            if (vector3s[1] == vector3s[vector3s.Length - 2])
            {
                Vector3f[] tmpLoopSpline = new Vector3f[vector3s.Length];
                Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
                tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
                tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
                vector3s = new Vector3f[tmpLoopSpline.Length];
                Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
            }

            return vector3s;
        }
        public static Vector3f Interp(Vector3f[] pts, float t)
        {
            int numSections = pts.Length - 3;
            int currPt = UtilMath.Min((int)Math.Floor(t * (float)numSections), numSections - 1);
            float u = t * (float)numSections - (float)currPt;

            Vector3f a = pts[currPt];
            Vector3f b = pts[currPt + 1];
            Vector3f c = pts[currPt + 2];
            Vector3f d = pts[currPt + 3];

            return .5f * (
                (-a + 3f * b - 3f * c + d) * (u * u * u)
                + (2f * a - 5f * b + 4f * c - d) * (u * u)
                + (-a + c) * u
                + 2f * b
            );
        }

        /// <summary>  
        /// Easing equation function for a simple linear tweening, with no easing.  
        /// </summary>  
        /// <param name="t">Current time in seconds.</param>  
        /// <param name="b">Starting value.</param>  
        /// <param name="c">Final value.</param>  
        /// <param name="d">Duration of animation.</param>  
        /// <returns>The correct value.</returns>
        public static float Evaluate(TweenStyle style, float t, float b, float c, float d)
        {
            switch (style)
            {
                default:
                case TweenStyle.Linear:
                    return Linear(t, b, c, d);
                case TweenStyle.BackEaseIn:
                    return BackEaseIn(t, b, c, d);
                case TweenStyle.BackEaseInOut:
                    return BackEaseInOut(t, b, c, d);
                case TweenStyle.BackEaseOut:
                    return BackEaseOut(t, b, c, d);
                case TweenStyle.BackEaseOutIn:
                    return BackEaseOutIn(t, b, c, d);
                case TweenStyle.BounceEaseIn:
                    return BounceEaseIn(t, b, c, d);
                case TweenStyle.BounceEaseInOut:
                    return BounceEaseInOut(t, b, c, d);
                case TweenStyle.BounceEaseOut:
                    return BounceEaseOut(t, b, c, d);
                case TweenStyle.BounceEaseOutIn:
                    return BounceEaseOutIn(t, b, c, d);
                case TweenStyle.CircEaseIn:
                    return CircEaseIn(t, b, c, d);
                case TweenStyle.CircEaseInOut:
                    return CircEaseInOut(t, b, c, d);
                case TweenStyle.CircEaseOut:
                    return CircEaseOut(t, b, c, d);
                case TweenStyle.CircEaseOutIn:
                    return CircEaseOutIn(t, b, c, d);
                case TweenStyle.CubicEaseIn:
                    return CubicEaseIn(t, b, c, d);
                case TweenStyle.CubicEaseInOut:
                    return CubicEaseInOut(t, b, c, d);
                case TweenStyle.CubicEaseOut:
                    return CubicEaseOut(t, b, c, d);
                case TweenStyle.CubicEaseOutIn:
                    return CubicEaseOutIn(t, b, c, d);
                case TweenStyle.ElasticEaseIn:
                    return ElasticEaseIn(t, b, c, d);
                case TweenStyle.ElasticEaseInOut:
                    return ElasticEaseInOut(t, b, c, d);
                case TweenStyle.ElasticEaseOut:
                    return ElasticEaseOut(t, b, c, d);
                case TweenStyle.ElasticEaseOutIn:
                    return ElasticEaseOutIn(t, b, c, d);
                case TweenStyle.ExpoEaseIn:
                    return ExpoEaseIn(t, b, c, d);
                case TweenStyle.ExpoEaseInOut:
                    return ExpoEaseInOut(t, b, c, d);
                case TweenStyle.ExpoEaseOut:
                    return ExpoEaseOut(t, b, c, d);
                case TweenStyle.ExpoEaseOutIn:
                    return ExpoEaseOutIn(t, b, c, d);
                case TweenStyle.QuadEaseIn:
                    return QuadEaseIn(t, b, c, d);
                case TweenStyle.QuadEaseInOut:
                    return QuadEaseInOut(t, b, c, d);
                case TweenStyle.QuadEaseOut:
                    return QuadEaseOut(t, b, c, d);
                case TweenStyle.QuadEaseOutIn:
                    return QuadEaseOutIn(t, b, c, d);
                case TweenStyle.QuartEaseIn:
                    return QuartEaseIn(t, b, c, d);
                case TweenStyle.QuartEaseInOut:
                    return QuartEaseInOut(t, b, c, d);
                case TweenStyle.QuartEaseOut:
                    return QuartEaseOut(t, b, c, d);
                case TweenStyle.QuartEaseOutIn:
                    return QuartEaseOutIn(t, b, c, d);
                case TweenStyle.QuintEaseIn:
                    return QuintEaseIn(t, b, c, d);
                case TweenStyle.QuintEaseInOut:
                    return QuintEaseInOut(t, b, c, d);
                case TweenStyle.QuintEaseOut:
                    return QuintEaseOut(t, b, c, d);
                case TweenStyle.QuintEaseOutIn:
                    return QuintEaseOutIn(t, b, c, d);
                case TweenStyle.SineEaseIn:
                    return SineEaseIn(t, b, c, d);
                case TweenStyle.SineEaseInOut:
                    return SineEaseInOut(t, b, c, d);
                case TweenStyle.SineEaseOut:
                    return SineEaseOut(t, b, c, d);
                case TweenStyle.SineEaseOutIn:
                    return SineEaseOutIn(t, b, c, d);
            }
        }
        static float Linear(float t, float b, float c, float d)
        {
            return (c - b) * t / d + b;
        }
        static float ExpoEaseOut(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * (-UtilMath.Pow(2, -10 * t / d) + 1) + b;
        }
        static float ExpoEaseIn(float t, float b, float c, float d)
        {
            return (t == 0) ? b : c * UtilMath.Pow(2, 10 * (t / d - 1)) + b;
        }
        static float ExpoEaseInOut(float t, float b, float c, float d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * UtilMath.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-UtilMath.Pow(2, -10 * --t) + 2) + b;
        }
        static float ExpoEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return ExpoEaseOut(t * 2, b, c / 2, d);

            return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float CircEaseOut(float t, float b, float c, float d)
        {
            return c * UtilMath.Sqrt(1 - (t = t / d - 1) * t) + b;
        }
        static float CircEaseIn(float t, float b, float c, float d)
        {
            return -c * (UtilMath.Sqrt(1 - (t /= d) * t) - 1) + b;
        }
        static float CircEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return -c / 2 * (UtilMath.Sqrt(1 - t * t) - 1) + b;

            return c / 2 * (UtilMath.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }
        static float CircEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return CircEaseOut(t * 2, b, c / 2, d);

            return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float QuadEaseOut(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
        static float QuadEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }
        static float QuadEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }
        static float QuadEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuadEaseOut(t * 2, b, c / 2, d);

            return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float SineEaseOut(float t, float b, float c, float d)
        {
            return c * UtilMath.Sin(t / d * (UtilMath.PI / 2)) + b;
        }
        static float SineEaseIn(float t, float b, float c, float d)
        {
            return -c * UtilMath.Cos(t / d * (UtilMath.PI / 2)) + c + b;
        }
        static float SineEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * (UtilMath.Sin(UtilMath.PI * t / 2)) + b;

            return -c / 2 * (UtilMath.Cos(UtilMath.PI * --t / 2) - 2) + b;
        }
        static float SineEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return SineEaseOut(t * 2, b, c / 2, d);

            return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float CubicEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }
        static float CubicEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t + b;
        }
        static float CubicEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t + b;

            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }
        static float CubicEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return CubicEaseOut(t * 2, b, c / 2, d);

            return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float QuartEaseOut(float t, float b, float c, float d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }
        static float QuartEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t + b;
        }
        static float QuartEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t + b;

            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }
        static float QuartEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuartEaseOut(t * 2, b, c / 2, d);

            return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float QuintEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        static float QuintEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }
        static float QuintEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
        static float QuintEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuintEaseOut(t * 2, b, c / 2, d);
            return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float ElasticEaseOut(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * 0.3f;
            float s = p / 4;

            return (c * UtilMath.Pow(2, -10 * t) * UtilMath.Sin((t * d - s) * (2 * UtilMath.PI) / p) + c + b);
        }
        static float ElasticEaseIn(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * 0.3f;
            float s = p / 4;

            return -(c * UtilMath.Pow(2, 10 * (t -= 1)) * UtilMath.Sin((t * d - s) * (2 * UtilMath.PI) / p)) + b;
        }
        static float ElasticEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2f) == 2)
                return b + c;

            float p = d * (0.3f * 1.5f);
            float s = p / 4;

            if (t < 1)
                return -0.5f * (c * UtilMath.Pow(2, 10 * (t -= 1)) * UtilMath.Sin((t * d - s) * (2 * UtilMath.PI) / p)) + b;
            return c * UtilMath.Pow(2, -10 * (t -= 1)) * UtilMath.Sin((t * d - s) * (2 * UtilMath.PI) / p) * 0.5f + c + b;
        }
        static float ElasticEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return ElasticEaseOut(t * 2, b, c / 2, d);
            return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float BounceEaseOut(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75f))
                return c * (7.5625f * t * t) + b;
            else if (t < (2 / 2.75f))
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
            else if (t < (2.5f / 2.75f))
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f) + b;
            else
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }
        static float BounceEaseIn(float t, float b, float c, float d)
        {
            return c - BounceEaseOut(d - t, 0, c, d) + b;
        }
        static float BounceEaseInOut(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BounceEaseIn(t * 2, 0, c, d) * 0.5f + b;
            else
                return BounceEaseOut(t * 2 - d, 0, c, d) * 0.5f + c * 0.5f + b;
        }
        static float BounceEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BounceEaseOut(t * 2, b, c / 2, d);
            return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
        static float BackEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
        }
        static float BackEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
        }
        static float BackEaseInOut(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1)
                return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }
        static float BackEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BackEaseOut(t * 2, b, c / 2, d);
            return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }
    }
}