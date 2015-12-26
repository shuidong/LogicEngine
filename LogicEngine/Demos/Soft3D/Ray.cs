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
    ///   <para>Representation of rays.</para>
    /// </summary>
    public struct Ray
    {
         Vector3f m_Origin;

         Vector3f m_Direction;

        /// <summary>
        ///   <para>The origin point of the ray.</para>
        /// </summary>
        public Vector3f origin
        {
            get
            {
                return this.m_Origin;
            }
            set
            {
                this.m_Origin = value;
            }
        }

        /// <summary>
        ///   <para>The direction of the ray.</para>
        /// </summary>
        public Vector3f direction
        {
            get
            {
                return this.m_Direction;
            }
            set
            {
                this.m_Direction = value.normalized;
            }
        }

        /// <summary>
        ///   <para>Creates a ray starting at origin along direction.</para>
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        public Ray(Vector3f origin, Vector3f direction)
        {
            this.m_Origin = origin;
            this.m_Direction = direction.normalized;
        }

        /// <summary>
        ///   <para>Returns a point at distance units along the ray.</para>
        /// </summary>
        /// <param name="distance"></param>
        public Vector3f GetPoint(float distance)
        {
            return this.m_Origin + this.m_Direction * distance;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this ray.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin,
				this.m_Direction
			});
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this ray.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format),
				this.m_Direction.ToString(format)
			});
        }
    }
}