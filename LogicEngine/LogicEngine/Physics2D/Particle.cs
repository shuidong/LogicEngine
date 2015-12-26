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

namespace LogicEngine.Physics2D
{
    public class Particle : UPart
    {
        public float mass { get; set; }
        public Vector2f position { get; set; }
        public Vector2f velocity { get; set; }
        public Vector2f acceleration { get; set; }
        public float maxSpeed { get; set; }

        protected override void _Awake()
        {
            mass = 1f;
            maxSpeed = float.MaxValue;
        }
        protected override void _Release()
        {
        }
        protected override void _Update(float sec)
        {
            velocity += sec * acceleration;
            velocity.Truncate(maxSpeed);
            position += sec * velocity;

            acceleration = Vector2f.zero;
            Notify();
        }

        public void AddForce(Vector2f force)
        {
            if (mass <= 0f)
            {
                UtilLog.LogError("Particle's Mass can't be 0");
                return;
            }
            this.acceleration += force / mass;
        }
    }
}