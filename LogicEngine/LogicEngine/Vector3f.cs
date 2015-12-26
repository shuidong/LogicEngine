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
    ///   <para>Representation of 3D vectors and points.</para>
    /// </summary>
    public struct Vector3f
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
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Vector3f normalized
        {
            get
            {
                return Vector3f.Normalize(this);
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return UtilMath.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return this.x * this.x + this.y * this.y + this.z * this.z;
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(0, 0, 0).</para>
        /// </summary>
        public static Vector3f zero
        {
            get
            {
                return new Vector3f(0f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(1, 1, 1).</para>
        /// </summary>
        public static Vector3f one
        {
            get
            {
                return new Vector3f(1f, 1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(0, 0, 1).</para>
        /// </summary>
        public static Vector3f forward
        {
            get
            {
                return new Vector3f(0f, 0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(0, 0, -1).</para>
        /// </summary>
        public static Vector3f back
        {
            get
            {
                return new Vector3f(0f, 0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(0, 1, 0).</para>
        /// </summary>
        public static Vector3f up
        {
            get
            {
                return new Vector3f(0f, 1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(0, -1, 0).</para>
        /// </summary>
        public static Vector3f down
        {
            get
            {
                return new Vector3f(0f, -1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(-1, 0, 0).</para>
        /// </summary>
        public static Vector3f left
        {
            get
            {
                return new Vector3f(-1f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3f(1, 0, 0).</para>
        /// </summary>
        public static Vector3f right
        {
            get
            {
                return new Vector3f(1f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets z to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector3f(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3f Lerp(Vector3f a, Vector3f b, float t)
        {
            t = UtilMath.Clamp01(t);
            return new Vector3f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3f LerpUnclamped(Vector3f a, Vector3f b, float t)
        {
            return new Vector3f(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Spherically interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3f Slerp(Vector3f a, Vector3f b, float t)
        {
            //Vector3f result;
            //Vector3f.INTERNAL_CALL_Slerp(ref a, ref b, t, out result);
            //return result;
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Spherically interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3f SlerpUnclamped(Vector3f a, Vector3f b, float t)
        {
            //Vector3f result;
            //Vector3f.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t, out result);
            //return result;
            throw new NotImplementedException();
        }


        public static void OrthoNormalize(ref Vector3f normal, ref Vector3f tangent)
        {
            throw new NotImplementedException();
        }

        public static void OrthoNormalize(ref Vector3f normal, ref Vector3f tangent, ref Vector3f binormal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Moves a point current in a straight line towards a target point.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Vector3f MoveTowards(Vector3f current, Vector3f target, float maxDistanceDelta)
        {
            Vector3f a = target - current;
            float magnitude = a.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }

        /// <summary>
        ///   <para>Rotates a vector current towards target.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxRadiansDelta"></param>
        /// <param name="maxMagnitudeDelta"></param>
        public static Vector3f RotateTowards(Vector3f current, Vector3f target, float maxRadiansDelta, float maxMagnitudeDelta)
        {
            throw new NotImplementedException();
        }

        public static Vector3f SmoothDamp(Vector3f current, Vector3f target, ref Vector3f currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = UtilMath.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vector3f vector = current - target;
            Vector3f vector2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vector3f Vector3f = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * Vector3f) * d;
            Vector3f vector4 = target + (vector + Vector3f) * d;
            if (Vector3f.Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing Vector3f.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        public void Set(float new_x, float new_y, float new_z)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector3f Scale(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector3f scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3f Cross(Vector3f lhs, Vector3f rhs)
        {
            return new Vector3f(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3f))
            {
                return false;
            }
            Vector3f vector = (Vector3f)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
        }

        /// <summary>
        ///   <para>Reflects a vector off the plane defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Vector3f Reflect(Vector3f inDirection, Vector3f inNormal)
        {
            return -2f * Vector3f.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Vector3f Normalize(Vector3f value)
        {
            float num = Vector3f.Magnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }
            return Vector3f.zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = Vector3f.Magnitude(this);
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = Vector3f.zero;
            }
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", new object[]
			{
				this.x,
				this.y,
				this.z
			});
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format)
			});
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3f lhs, Vector3f rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        public static Vector3f Project(Vector3f vector, Vector3f onNormal)
        {
            float num = Vector3f.Dot(onNormal, onNormal);
            if (num < UtilMath.Epsilon)
            {
                return Vector3f.zero;
            }
            return onNormal * Vector3f.Dot(vector, onNormal) / num;
        }

        /// <summary>
        ///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        public static Vector3f ProjectOnPlane(Vector3f vector, Vector3f planeNormal)
        {
            return vector - Vector3f.Project(vector, planeNormal);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Vector3f from, Vector3f to)
        {
            return UtilMath.Acos(UtilMath.Clamp(Vector3f.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector3f a, Vector3f b)
        {
            Vector3f vector = new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
            return UtilMath.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Vector3f ClampMagnitude(Vector3f vector, float maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }

        public static float Magnitude(Vector3f a)
        {
            return UtilMath.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static float SqrMagnitude(Vector3f a)
        {
            return a.x * a.x + a.y * a.y + a.z * a.z;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3f Min(Vector3f lhs, Vector3f rhs)
        {
            return new Vector3f(UtilMath.Min(lhs.x, rhs.x), UtilMath.Min(lhs.y, rhs.y), UtilMath.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3f Max(Vector3f lhs, Vector3f rhs)
        {
            return new Vector3f(UtilMath.Max(lhs.x, rhs.x), UtilMath.Max(lhs.y, rhs.y), UtilMath.Max(lhs.z, rhs.z));
        }

        public static Vector3f operator +(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3f operator -(Vector3f a)
        {
            return new Vector3f(-a.x, -a.y, -a.z);
        }

        public static Vector3f operator *(Vector3f a, float d)
        {
            return new Vector3f(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3f operator *(float d, Vector3f a)
        {
            return new Vector3f(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3f operator /(Vector3f a, float d)
        {
            return new Vector3f(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3f lhs, Vector3f rhs)
        {
            return Vector3f.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector3f lhs, Vector3f rhs)
        {
            return Vector3f.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }
    }
}