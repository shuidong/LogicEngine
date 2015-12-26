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
using LogicEngine;

namespace Demos.Soft3D
{
    /// <summary>
    ///   <para>Representation of four-dimensional vectors.</para>
    /// </summary>
    public struct Vector4f
    {
        public const float kEpsilon = 1E-05f;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float z;

        /// <summary>
        ///   <para>W component of the vector.</para>
        /// </summary>
        public float w;

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Vector4f normalized
        {
            get
            {
                return Vector4f.Normalize(this);
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return UtilMath.Sqrt(Vector4f.Dot(this, this));
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return Vector4f.Dot(this, this);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector4f(0,0,0,0).</para>
        /// </summary>
        public static Vector4f zero
        {
            get
            {
                return new Vector4f(0f, 0f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector4f(1,1,1,1).</para>
        /// </summary>
        public static Vector4f one
        {
            get
            {
                return new Vector4f(1f, 1f, 1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z, w components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4f(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components and sets w to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector4f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0f;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets z and w to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector4f(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
            this.w = 0f;
        }

        /// <summary>
        ///   <para>Set x, y, z and w components of an existing Vector4f.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        /// <param name="new_w"></param>
        public void Set(float new_x, float new_y, float new_z, float new_w)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
            this.w = new_w;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector4f Lerp(Vector4f a, Vector4f b, float t)
        {
            t = UtilMath.Clamp01(t);
            return new Vector4f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector4f LerpUnclamped(Vector4f a, Vector4f b, float t)
        {
            return new Vector4f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
        }

        /// <summary>
        ///   <para>Moves a point current towards target.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Vector4f MoveTowards(Vector4f current, Vector4f target, float maxDistanceDelta)
        {
            Vector4f a = target - current;
            float magnitude = a.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector4f Scale(Vector4f a, Vector4f b)
        {
            return new Vector4f(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector4f scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
            this.w *= scale.w;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector4f))
            {
                return false;
            }
            Vector4f vector = (Vector4f)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="a"></param>
        public static Vector4f Normalize(Vector4f a)
        {
            float num = Vector4f.Magnitude(a);
            if (num > 1E-05f)
            {
                return a / num;
            }
            return Vector4f.zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = Vector4f.Magnitude(this);
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = Vector4f.zero;
            }
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format),
				this.w.ToString(format)
			});
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Vector4f a, Vector4f b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector4f Project(Vector4f a, Vector4f b)
        {
            return b * Vector4f.Dot(a, b) / Vector4f.Dot(b, b);
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector4f a, Vector4f b)
        {
            return Vector4f.Magnitude(a - b);
        }

        public static float Magnitude(Vector4f a)
        {
            return UtilMath.Sqrt(Vector4f.Dot(a, a));
        }

        public static float SqrMagnitude(Vector4f a)
        {
            return Vector4f.Dot(a, a);
        }

        public float SqrMagnitude()
        {
            return Vector4f.Dot(this, this);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector4f Min(Vector4f lhs, Vector4f rhs)
        {
            return new Vector4f(UtilMath.Min(lhs.x, rhs.x), UtilMath.Min(lhs.y, rhs.y), UtilMath.Min(lhs.z, rhs.z), UtilMath.Min(lhs.w, rhs.w));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector4f Max(Vector4f lhs, Vector4f rhs)
        {
            return new Vector4f(UtilMath.Max(lhs.x, rhs.x), UtilMath.Max(lhs.y, rhs.y), UtilMath.Max(lhs.z, rhs.z), UtilMath.Max(lhs.w, rhs.w));
        }

        public static Vector4f operator +(Vector4f a, Vector4f b)
        {
            return new Vector4f(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4f operator -(Vector4f a, Vector4f b)
        {
            return new Vector4f(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4f operator -(Vector4f a)
        {
            return new Vector4f(-a.x, -a.y, -a.z, -a.w);
        }

        public static Vector4f operator *(Vector4f a, float d)
        {
            return new Vector4f(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4f operator *(float d, Vector4f a)
        {
            return new Vector4f(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        public static Vector4f operator /(Vector4f a, float d)
        {
            return new Vector4f(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        public static bool operator ==(Vector4f lhs, Vector4f rhs)
        {
            return Vector4f.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector4f lhs, Vector4f rhs)
        {
            return Vector4f.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }

        public static implicit operator Vector4f(Vector3f v)
        {
            return new Vector4f(v.x, v.y, v.z, 0f);
        }

        public static implicit operator Vector3f(Vector4f v)
        {
            return new Vector3f(v.x, v.y, v.z);
        }

        public static implicit operator Vector4f(Vector2f v)
        {
            return new Vector4f(v.x, v.y, 0f, 0f);
        }

        public static implicit operator Vector2f(Vector4f v)
        {
            return new Vector2f(v.x, v.y);
        }
    }
}