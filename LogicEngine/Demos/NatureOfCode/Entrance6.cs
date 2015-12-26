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
using UnityEngine;
using LogicEngine;
using LogicEngine.Unity;
using LogicEngine.Physics2D;

namespace Demos.NatureOfCode
{
    public class Entrance6 : Entrance
    {
        protected override void _Init()
        {
            var app = Logic.AddChild().AddPart<App>();

            var scene_mgr = app.entity.AddPart<SceneMgr>();
            scene_mgr.Register<Scene>("Scene", app);
            scene_mgr.Switch("Scene");
        }

        class Scene : LogicEngine.Scene<App>
        {
            protected override IEnumerator _Awake()
            {
                Part.boids.Attach(OnAddBoid);
                UiMgr.UiSketch.Show();
                yield return null;
            }
            void OnAddBoid(Boid walker)
            {
                walker.entity.AddPart<CBoid>();
            }
        }
        class CBoid : CPart<Boid>
        {
            GameObject go;
            Color mColor;
            protected override void _Awake()
            {
                go = UtilResource.LoadPrefab("Capsule");
                Part.particle.Attach(_OnChange);
                mColor = new Color(UtilRandom.Range01(), UtilRandom.Range01(), UtilRandom.Range01(), 1f);
            }
            protected override void _Release()
            {
                Part.particle.Detach(_OnChange);
            }
            protected override void _OnChange()
            {
                go.transform.position = new Vector3(Part.position.x, 0f, Part.position.y);
                UiMgr.UiSketch.DrawPoint((int)Part.position.x, (int)Part.position.y, mColor);
            }
        }

        class App : UPart
        {
            public EntityMgr<Boid> boids { get; private set; }

            SteerType mSteerType { get; set; }
            Boid mBoid0;
            Boid mBoid1;

            LogicEngine.Physics2D.Rect mRect;
            FlowField mFlowField;
            Path mPath;

            protected override void _Awake()
            {
                boids = AddPart<EntityMgr<Boid>>();
                Switch(SteerType.Cellular);
                Entrance6.Instance.AddDebugBtn("切换", OnSwitchBtn);
            }
            void OnSwitchBtn()
            {
                Switch((SteerType)(((int)mSteerType + 1) % Enum.GetNames(typeof(SteerType)).Length));
            }
            protected override void _Release()
            {
            }

            protected override void _Update(float sec)
            {
                switch (mSteerType)
                {
                    case SteerType.Seek:
                        {
                            mBoid0.Seek(new Vector2f(Input.mousePosition.x, Input.mousePosition.y));
                        }
                        break;
                    case SteerType.Arrive:
                        {
                            mBoid0.Arrive(new Vector2f(Input.mousePosition.x, Input.mousePosition.y), 60);
                        }
                        break;
                    case SteerType.Wander:
                        {
                            mBoid0.Wander(Vector2f.zero, 30);
                        }
                        break;
                    case SteerType.StayWithinFlowField:
                        {
                            mBoid0.StayWithinFlowField(mFlowField);
                        }
                        break;
                    case SteerType.Pursuit:
                        {
                            mBoid0.Pursuit(mBoid1, 18);
                            mBoid1.StayWithinRect(mRect);
                        }
                        break;
                }

                UiMgr.UiSketch.Apply();
            }
            void Switch(SteerType type)
            {
                boids.Clear();
                UiMgr.UiSketch.Clear(Color.black);
                UiMgr.UiSketch.DrawString("abcdefghijkmnlopqtsv", Color.red);
                mSteerType = type;
                switch (type)
                {
                    case SteerType.Seek:
                        {
                            mBoid0 = boids.Add();
                            mBoid0.position = GetScreenPosition();
                            mBoid0.velocity = new Vector2f(60, 0);
                            mBoid0.maxSpeed = 65;
                            mBoid0.maxForce = 50;
                        }
                        break;
                    case SteerType.Arrive:
                        {
                            mBoid0 = boids.Add();
                            mBoid0.position = GetScreenPosition();
                            mBoid0.velocity = new Vector2f(60, 0);
                            mBoid0.maxSpeed = 65;
                            mBoid0.maxForce = 50;
                        }
                        break;
                    case SteerType.Wander:
                        {
                            mBoid0 = boids.Add();
                            mBoid0.position = GetScreenPosition();
                            mBoid0.velocity = new Vector2f(60, 0);
                            mBoid0.maxSpeed = 65;
                            mBoid0.maxForce = 50;
                        }
                        break;
                    case SteerType.StayWithinFlowField:
                        {
                            mFlowField = new FlowField(new Vector2i(20, 10), new Vector2f(30, 30), new Vector2f(Screen.width * 0.5f, Screen.height * 0.5f));
                            mFlowField.Random();
                            UiMgr.UiSketch.DrawFlowField(mFlowField, Color.blue);

                            mBoid0 = boids.Add();
                            mBoid0.position = GetScreenPosition();
                            mBoid0.velocity = new Vector2f(60, 0);
                            mBoid0.maxSpeed = 65;
                            mBoid0.maxForce = 50;
                        }
                        break;
                    case SteerType.Pursuit:
                        {
                            mBoid0 = boids.Add();
                            mBoid0.position = GetScreenPosition();
                            mBoid0.velocity = new Vector2f(60, 0);
                            mBoid0.maxSpeed = 65;
                            mBoid0.maxForce = 50;

                            mRect = new LogicEngine.Physics2D.Rect(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f);
                            UiMgr.UiSketch.DrawRect(mRect, Color.green);

                            mBoid1 = boids.Add();
                            mBoid1.position = GetScreenPosition() + Vector2f.one * 200f;
                            mBoid1.velocity = new Vector2f(60, 0);
                            mBoid1.maxSpeed = 65;
                            mBoid1.maxForce = 50;
                        }
                        break;
                    case SteerType.Cellular:
                        {
                            var cellular = new LogicEngine.Physics2D.Cellular1(80, 30);
                            //cellular.Run(90);
                            //cellular.Run(110);
                            cellular.Run(30);
                            UiMgr.UiSketch.DrawCellular(cellular, GetScreenPosition(), Vector2f.one * 10, Color.green, Color.cyan);
                        }
                        break;
                }
            }
            Vector2f GetScreenPosition()
            {
                return new Vector2f((Screen.width * 0.5f), (Screen.height * 0.5f));
            }

            public enum SteerType
            {
                Seek,
                Arrive,
                Wander,
                StayWithinFlowField,
                Pursuit,

                Cellular,
            }
        }
    }
}