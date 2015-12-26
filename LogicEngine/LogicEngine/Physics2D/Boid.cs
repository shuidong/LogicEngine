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

namespace LogicEngine.Physics2D
{
    /// <summary>
    /// Steering Behaviours
    /// </summary>
    public class Boid : Part
    {
        public Particle particle { get; private set; }
        public float mass { get { return particle.mass; } set { particle.mass = value; } }
        public Vector2f position { get { return particle.position; } set { particle.position = value; } }
        public Vector2f velocity { get { return particle.velocity; } set { particle.velocity = value; } }
        public Vector2f acceleration { get { return particle.acceleration; } }
        public float maxSpeed { get { return particle.maxSpeed; } set { particle.maxSpeed = value; } }
        public float maxForce { get; set; }
        public float maxTurnRate { get; set; }

        protected override void _Awake()
        {
            particle = entity.GetOrAddPart<Particle>();
        }
        protected override void _Release()
        {
        }

        public void Seek(Vector2f target)
        {
            var desired = (target - position).normalized * maxSpeed;
            Steer(desired);
        }
        public void Flee(Vector2f target)
        {
            var desired = (position - target).normalized * maxSpeed;
            Steer(desired);
        }
        public void Arrive(Vector2f target, float decelerate_radius)
        {
            var desired = target - position;
            desired = desired.normalized * UtilMath.Lerp(0, maxSpeed, desired.magnitude / decelerate_radius);

            Steer(desired);
        }
        public void Wander(Vector2f local_target, float wander_radius)
        {
            var desired = (local_target + UtilRandom.onUnitCircle * wander_radius).normalized * maxSpeed;

            Steer(desired);
        }
        public void StayWithinRect(Rect rect)
        {
            if (position.x < rect.xMin)
            {
                Steer(new Vector2f(maxSpeed, velocity.y));
            }
            else if (position.x > rect.xMax)
            {
                Steer(new Vector2f(-maxSpeed, velocity.y));
            }
            if (position.y < rect.yMin)
            {
                Steer(new Vector2f(velocity.x, maxSpeed));
            }
            else if (position.y > rect.yMax)
            {
                Steer(new Vector2f(velocity.x, -maxSpeed));
            }
        }
        public void StayWithinFlowField(FlowField field)
        {
            if (field.Contains(position))
            {
                Steer(field.GetDir(position) * maxSpeed);
            }
            else
            {
                Steer((field.Center - position).normalized * maxSpeed);
            }
        }
        public void FollowPath(Path path)
        {
            var predict = position + 0.2f * velocity;
            var normal_point = UtilGeometry.GetNormalPoint(predict, path.First, path.Last);
            if (Vector2f.Distance(predict, normal_point) > path.Radius)
            {
                Seek(normal_point + (path.Last - path.First).normalized * 20);
            }
        }
        public void Pursuit(Boid evader, float angel)
        {
            var heading = velocity.normalized;
            var evader_heading = evader.velocity.normalized;
            var to_evader = evader.position - position;

            float relative_heading = Vector2f.Dot(heading, evader_heading);
            if (relative_heading < UtilMath.Cos(angel * UtilMath.Deg2Rad) &&
                Vector2f.Dot(to_evader.normalized, heading) > 0)
            {
                Seek(evader.position);
            }
            else
            {
                float secs = to_evader.magnitude / (evader.velocity.magnitude + maxSpeed);
                Seek(evader.position + evader.velocity * secs);
            }
        }
        public void Evade(Boid pursuer, float angel)
        {
            var topursuer = (pursuer.position - position);
            float secs = topursuer.magnitude / (pursuer.velocity.magnitude + maxSpeed);
            Flee(pursuer.position + pursuer.velocity * secs);
        }
        void Steer(Vector2f desired)
        {
            var steer = desired - velocity;
            steer.Truncate(maxForce);
            particle.AddForce(steer);
        }
    }
}