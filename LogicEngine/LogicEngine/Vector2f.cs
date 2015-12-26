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
    ///   <para>Representation of 2D vectors and points.</para>
    /// </summary>
    public struct Vector2f
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
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Vector2f normalized
        {
            get
            {
                Vector2f result = new Vector2f(this.x, this.y);
                result.Normalize();
                return result;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return UtilMath.Sqrt(this.x * this.x + this.y * this.y);
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return this.x * this.x + this.y * this.y;
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(0, 0).</para>
        /// </summary>
        public static Vector2f zero
        {
            get
            {
                return new Vector2f(0f, 0f);
            }
        }
        /// <summary>
        ///   <para>Shorthand for writing Vector2f(0.5f, 0.5f).</para>
        /// </summary>
        public static Vector2f half
        {
            get
            {
                return new Vector2f(0.5f, 0.5f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(1, 1).</para>
        /// </summary>
        public static Vector2f one
        {
            get
            {
                return new Vector2f(1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(0, 1).</para>
        /// </summary>
        public static Vector2f up
        {
            get
            {
                return new Vector2f(0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(0, -1).</para>
        /// </summary>
        public static Vector2f down
        {
            get
            {
                return new Vector2f(0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(-1, 0).</para>
        /// </summary>
        public static Vector2f left
        {
            get
            {
                return new Vector2f(-1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2f(1, 0).</para>
        /// </summary>
        public static Vector2f right
        {
            get
            {
                return new Vector2f(1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Constructs a new vector with given x, y components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        ///   <para>Set x and y components of an existing Vector2f.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        public void Set(float new_x, float new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector2f Lerp(Vector2f a, Vector2f b, float t)
        {
            t = UtilMath.Clamp01(t);
            return new Vector2f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector2f LerpUnclamped(Vector2f a, Vector2f b, float t)
        {
            return new Vector2f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Moves a point current towards target.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Vector2f MoveTowards(Vector2f current, Vector2f target, float maxDistanceDelta)
        {
            Vector2f a = target - current;
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
        public static Vector2f Scale(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector2f scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if (magnitude > kEpsilon)
            {
                this /= magnitude;
            }
            else
            {
                this = Vector2f.zero;
            }
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", new object[]
			{
				this.x,
				this.y
			});
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format)
			});
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2f))
            {
                return false;
            }
            Vector2f vector = (Vector2f)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y);
        }

        /// <summary>
        ///   <para>Reflects a vector off the vector defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Vector2f Reflect(Vector2f inDirection, Vector2f inNormal)
        {
            return -2f * Vector2f.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector2f lhs, Vector2f rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static float Angle(Vector2f from, Vector2f to)
        {
            return UtilMath.Acos(UtilMath.Clamp(Vector2f.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector2f a, Vector2f b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Vector2f ClampMagnitude(Vector2f vector, float maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        public static float SqrMagnitude(Vector2f a)
        {
            return a.x * a.x + a.y * a.y;
        }

        public float SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2f Min(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(UtilMath.Min(lhs.x, rhs.x), UtilMath.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2f Max(Vector2f lhs, Vector2f rhs)
        {
            return new Vector2f(UtilMath.Max(lhs.x, rhs.x), UtilMath.Max(lhs.y, rhs.y));
        }

        public static Vector2f SmoothDamp(Vector2f current, Vector2f target, ref Vector2f currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = UtilMath.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vector2f vector = current - target;
            Vector2f Vector2f = target;
            float maxLength = maxSpeed * smoothTime;
            vector = Vector2f.ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vector2f vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            Vector2f vector4 = target + (vector + vector3) * d;
            if (Vector2f.Dot(Vector2f - current, vector4 - Vector2f) > 0f)
            {
                vector4 = Vector2f;
                currentVelocity = (vector4 - Vector2f) / deltaTime;
            }
            return vector4;
        }

        public static Vector2f operator +(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x + b.x, a.y + b.y);
        }

        public static Vector2f operator -(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x - b.x, a.y - b.y);
        }

        public static Vector2f operator -(Vector2f a)
        {
            return new Vector2f(-a.x, -a.y);
        }

        public static Vector2f operator *(Vector2f a, float d)
        {
            return new Vector2f(a.x * d, a.y * d);
        }

        public static Vector2f operator *(float d, Vector2f a)
        {
            return new Vector2f(a.x * d, a.y * d);
        }

        public static Vector2f operator /(Vector2f a, float d)
        {
            return new Vector2f(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2f lhs, Vector2f rhs)
        {
            return Vector2f.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector2f lhs, Vector2f rhs)
        {
            return Vector2f.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }

        public static implicit operator Vector2f(Vector3f v)
        {
            return new Vector2f(v.x, v.y);
        }

        public static implicit operator Vector3f(Vector2f v)
        {
            return new Vector3f(v.x, v.y, 0f);
        }
        public static implicit operator Vector2f(Vector2i v)
        {
            return new Vector2f(v.x, v.y);
        }

        public void Truncate(float max)
        {
            var mag = magnitude;
            if (mag > max)
            {
                this /= (mag / max);
            }
        }
        public Vector2i RoundToVector2i()
        {
            return new Vector2i(UtilMath.RoundToInt(x), UtilMath.RoundToInt(y));
        }
    }
}