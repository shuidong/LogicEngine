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
    public class SceneEditIslet : Scene<Islet>
    {
        GameObject mGameObject;
        LogicEngine.Unity.Toolkit.FOV45 mFov45;

        protected override IEnumerator _Awake()
        {
            Part.entity.AddPart<Parts.CIslet>();
            Part.buildings.Attach(OnAddBuilding);
            Part.varias.Attach(OnAddVaria);
            mGameObject = UtilGameObject.Create("EditIslet");
            mGameObject.transform.SetAsFirstSibling();
            mGameObject.AddComponent<GizmoTileMap>().SetTile(Part.tilemap, Islet.Mask.Blocked);
            mGameObject.AddComponent<EditMenu>().Attach(this);
            mFov45 = Camera.main.GetComponent<LogicEngine.Unity.Toolkit.FOV45>();
            mFov45.Zoom(12);
            yield return null;
        }
        void OnAddBuilding(Building building)
        {
            building.entity.AddPart<Parts.CBuilding>();
        }
        void OnAddVaria(Varia varia)
        {
            varia.entity.AddPart<Parts.CVaria>();
        }
        void OnEditAddBuilding(Vector2 screen_position, string cfg_id)
        {
            var tile = Part.tilemap.ToTile(Camera.main.Screen2Ground(Input.mousePosition).to2());
            if (Part.tilemap.Contains(tile))
            {
                DycBuilding dyc = UtilRandom.Dyc<DycBuilding>();
                dyc.d0 = cfg_id;
                dyc.d1 = 1;
                dyc.d2x = tile.x;
                dyc.d2y = tile.y;
                Part.buildings.Add(dyc);
            }
        }
        void OnEditAddVaria(Vector2 screen_position, string cfg_id)
        {
            var tile = Part.tilemap.ToTile(Camera.main.Screen2Ground(Input.mousePosition).to2());
            if (Part.tilemap.Contains(tile))
            {
                DycVaria dyc = UtilRandom.Dyc<DycVaria>();
                dyc.d0 = cfg_id;
                dyc.d2x = tile.x;
                dyc.d2y = tile.y;
                Part.varias.Add(dyc);
            }
        }

        class GizmoTileMap : LogicEngine.Unity.GizmoTileMap<Islet.Mask>
        {
        }
        class EditMenu : ForEdit
        {
            bool mSelectBuilding;
            int mIndex;
            List<CfgBuilding> mBuildings;
            string[] mBuildingNames;
            LogicEngine.Unity.Toolkit.FOV45 mFov45;
            void Awake()
            {
                mBuildings = CfgMgr.Buildings.ToList();
                mBuildingNames = mBuildings.Map(it=> it.name).ToArray();
                mFov45 = Camera.main.GetComponent<LogicEngine.Unity.Toolkit.FOV45>();
            }
            void Update()
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    mSelectBuilding = !mSelectBuilding;
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
                {
                    SendEditMessage("OnEditAddBuilding", (Vector2)Input.mousePosition, mBuildings[mIndex].id);
                }
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
            }

            void OnGUI()
            {
                if (mSelectBuilding)
                {
                    var height = 30;
                    var count_y = Screen.height / height;
                    var count_x = mBuildingNames.Length / count_y + 1;
                    mIndex = GUI.SelectionGrid(new Rect(0, 0, 100 * count_x, height * count_y), mIndex, mBuildingNames, count_x);
                }
            }
        }
    }
}