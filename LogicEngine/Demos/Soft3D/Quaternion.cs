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
    ///   <para>Quaternions are used to represent rotations.</para>
    /// </summary>
    public struct Quaternion
    {
        public const float kEpsilon = 1E-06f;

        /// <summary>
        ///   <para>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float z;

        /// <summary>
        ///   <para>W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float w;

        /// <summary>
        ///   <para>The identity rotation (Read Only).</para>
        /// </summary>
        public static Quaternion identity
        {
            get
            {
                return new Quaternion(0f, 0f, 0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Returns the euler angle representation of the rotation.</para>
        /// </summary>
        public Vector3f eulerAngles
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///   <para>Constructs new Quaternion with given x,y,z,w components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        ///   <para>Set x, y, z and w components of an existing Quaternion.</para>
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
        ///   <para>The dot product between two rotations.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates angle degrees around axis.</para>
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public static Quaternion AngleAxis(float angle, Vector3f axis)
        {
            throw new NotImplementedException();
        }

        public void ToAngleAxis(out float angle, out Vector3f axis)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates from fromDirection to toDirection.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public static Quaternion FromToRotation(Vector3f fromDirection, Vector3f toDirection)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///   <para>Creates a rotation which rotates from fromDirection to toDirection.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public void SetFromToRotation(Vector3f fromDirection, Vector3f toDirection)
        {
            this = Quaternion.FromToRotation(fromDirection, toDirection);
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="forward">The direction to look in.</param>
        /// <param name="upwards">The vector that defines in which direction up is.</param>
        public static Quaternion LookRotation(Vector3f forward, Vector3f upwards)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="forward">The direction to look in.</param>
        /// <param name="upwards">The vector that defines in which direction up is.</param>
        public static Quaternion LookRotation(Vector3f forward)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="view">The direction to look in.</param>
        /// <param name="up">The vector that defines in which direction up is.</param>
        public void SetLookRotation(Vector3f view)
        {
            Vector3f up = Vector3f.up;
            this.SetLookRotation(view, up);
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="view">The direction to look in.</param>
        /// <param name="up">The vector that defines in which direction up is.</param>
        public void SetLookRotation(Vector3f view, Vector3f up)
        {
            this = Quaternion.LookRotation(view, up);
        }

        /// <summary>
        ///   <para>Spherically interpolates between a and b by t. The parameter t is clamped to the range [0, 1].</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Spherically interpolates between a and b by t. The parameter t is not clamped.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Interpolates between a and b by t and normalizes the result afterwards. The parameter t is clamped to the range [0, 1].</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Interpolates between a and b by t and normalizes the result afterwards. The parameter t is not clamped.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Rotates a rotation from towards to.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="maxDegreesDelta"></param>
        public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
        {
            float num = Quaternion.Angle(from, to);
            if (num == 0f)
            {
                return to;
            }
            float t = UtilMath.Min(1f, maxDegreesDelta / num);
            return Quaternion.SlerpUnclamped(from, to, t);
        }

        /// <summary>
        ///   <para>Returns the Inverse of rotation.</para>
        /// </summary>
        /// <param name="rotation"></param>
        public static Quaternion Inverse(Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
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
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
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
        ///   <para>Returns the angle in degrees between two rotations a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Angle(Quaternion a, Quaternion b)
        {
            float f = Quaternion.Dot(a, b);
            return UtilMath.Acos(UtilMath.Min(UtilMath.Abs(f), 1f)) * 2f * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static Quaternion Euler(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="euler"></param>
        public static Quaternion Euler(Vector3f euler)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Quaternion))
            {
                return false;
            }
            Quaternion quaternion = (Quaternion)other;
            return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3f operator *(Quaternion rotation, Vector3f point)
        {
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            Vector3f result;
            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return result;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return Quaternion.Dot(lhs, rhs) > 0.999999f;
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return Quaternion.Dot(lhs, rhs) <= 0.999999f;
        }
    }
}