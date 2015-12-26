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
using LogicEngine.Unity;
using UnityEngine;

namespace Demos.BoomBeach.Client.Scenes
{
    public class SceneMainbase : Scene<Play>
    {
        LogicEngine.Unity.Toolkit.FOV45 mFov45;

        protected override IEnumerator _Awake()
        {
            UtilAssert.IsNotNull(Part, "Part null");
            //var hero = Part.core.heros.Find(it => true);
            //hero.entity.AddPart<CHeroPanel>();

            //Part.mainbase.islet.buildings.Attach(OnAddBuilding);
            //Part.mainbase.islet.varias.Attach(OnAddVaria);
            //Part.mainbase.islet.coolies.Attach(OnAddCooly);

            mFov45 = Camera.main.GetComponent<LogicEngine.Unity.Toolkit.FOV45>();
            mFov45.Zoom(12);

            //Part.mainbase.islet.entity.AddPart<CParts.CIslet>();
            yield return null;

            var go = new GameObject("TEMP");
            //go.AddComponent<GizmoTileMap>().SetTile(Part.mainbase.islet.tilemap, Islet.Mask.Blocked);
            go.AddComponent<MonoUpdate>().Scene = this;
        }

        void OnAddBuilding(Building building)
        {
            building.entity.AddPart<Parts.CBuilding>();
        }
        void OnAddVaria(Varia varia)
        {
            varia.entity.AddPart<Parts.CVaria>();
        }
        void OnAddCooly(Cooly cooly)
        {
            cooly.entity.AddPart<Parts.CCooly>();
        }
        class GizmoTileMap : LogicEngine.Unity.GizmoTileMap<Islet.Mask>
        {
        }

        class MonoUpdate : MonoBehaviour
        {
            LogicEngine.Unity.Toolkit.FOV45 mFov45;
            public SceneMainbase Scene;

            void Awake()
            {
                mFov45 = Camera.main.GetComponent<LogicEngine.Unity.Toolkit.FOV45>();
            }
            void Update()
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    mFov45.FocusOnGround(Camera.main.Screen2Ground(Input.mousePosition));
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    mFov45.Zoom(Vector3.Distance(Camera.main.transform.position, Camera.main.Screen2Ground(UtilScreen.GetScreenCenter())) + 10f);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    mFov45.Zoom(Vector3.Distance(Camera.main.transform.position, Camera.main.Screen2Ground(UtilScreen.GetScreenCenter())) - 10f);
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    var c = Scene.Part.mainbase.islet.coolies.Find(it => true);
                    c.IsBattle = !c.IsBattle;
                    Debug.LogWarning(c.IsBattle);
                }
            }
        }
    }
}