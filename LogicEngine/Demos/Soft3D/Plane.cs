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
    ///   <para>Representation of a plane in 3D space.</para>
    /// </summary>
    public struct Plane
    {
         Vector3f m_Normal;

         float m_Distance;

        /// <summary>
        ///   <para>Normal vector of the plane.</para>
        /// </summary>
        public Vector3f normal
        {
            get
            {
                return this.m_Normal;
            }
            set
            {
                this.m_Normal = value;
            }
        }

        /// <summary>
        ///   <para>Distance from the origin to the plane.</para>
        /// </summary>
        public float distance
        {
            get
            {
                return this.m_Distance;
            }
            set
            {
                this.m_Distance = value;
            }
        }

        /// <summary>
        ///   <para>Creates a plane.</para>
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="inPoint"></param>
        public Plane(Vector3f inNormal, Vector3f inPoint)
        {
            this.m_Normal = Vector3f.Normalize(inNormal);
            this.m_Distance = -Vector3f.Dot(inNormal, inPoint);
        }

        /// <summary>
        ///   <para>Creates a plane.</para>
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="d"></param>
        public Plane(Vector3f inNormal, float d)
        {
            this.m_Normal = Vector3f.Normalize(inNormal);
            this.m_Distance = d;
        }

        /// <summary>
        ///   <para>Creates a plane.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Plane(Vector3f a, Vector3f b, Vector3f c)
        {
            this.m_Normal = Vector3f.Normalize(Vector3f.Cross(b - a, c - a));
            this.m_Distance = -Vector3f.Dot(this.m_Normal, a);
        }

        /// <summary>
        ///   <para>Sets a plane using a point that lies within it along with a normal to orient it.</para>
        /// </summary>
        /// <param name="inNormal">The plane's normal vector.</param>
        /// <param name="inPoint">A point that lies on the plane.</param>
        public void SetNormalAndPosition(Vector3f inNormal, Vector3f inPoint)
        {
            this.normal = Vector3f.Normalize(inNormal);
            this.distance = -Vector3f.Dot(inNormal, inPoint);
        }

        /// <summary>
        ///   <para>Sets a plane using three points that lie within it.  The points go around clockwise as you look down on the top surface of the plane.</para>
        /// </summary>
        /// <param name="a">First point in clockwise order.</param>
        /// <param name="b">Second point in clockwise order.</param>
        /// <param name="c">Third point in clockwise order.</param>
        public void Set3Points(Vector3f a, Vector3f b, Vector3f c)
        {
            this.normal = Vector3f.Normalize(Vector3f.Cross(b - a, c - a));
            this.distance = -Vector3f.Dot(this.normal, a);
        }

        /// <summary>
        ///   <para>Returns a signed distance from plane to point.</para>
        /// </summary>
        /// <param name="inPt"></param>
        public float GetDistanceToPoint(Vector3f inPt)
        {
            return Vector3f.Dot(this.normal, inPt) + this.distance;
        }

        /// <summary>
        ///   <para>Is a point on the positive side of the plane?</para>
        /// </summary>
        /// <param name="inPt"></param>
        public bool GetSide(Vector3f inPt)
        {
            return Vector3f.Dot(this.normal, inPt) + this.distance > 0f;
        }

        /// <summary>
        ///   <para>Are two points on the same side of the plane?</para>
        /// </summary>
        /// <param name="inPt0"></param>
        /// <param name="inPt1"></param>
        public bool SameSide(Vector3f inPt0, Vector3f inPt1)
        {
            float distanceToPoint = this.GetDistanceToPoint(inPt0);
            float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
            return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
        }

        public bool Raycast(Ray ray, out float enter)
        {
            float num = Vector3f.Dot(ray.direction, this.normal);
            float num2 = -Vector3f.Dot(ray.origin, this.normal) - this.distance;
            if (UtilMath.Approximately(num, 0f))
            {
                enter = 0f;
                return false;
            }
            enter = num2 / num;
            return enter > 0f;
        }
    }
}