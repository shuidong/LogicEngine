//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LogicEngine.Unity
{
    public static class UtilUi
    {
        public static bool IsTouchUi { get { return EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.layer == (int)GameLayer.UI; } }

        static Canvas Canvas2;
        static Dictionary<UiLayer, RectTransform> mLayers = new Dictionary<UiLayer, RectTransform>();
        //static Stack<IUi> mUiStack = new Stack<IUi>();

        static Canvas Canvas3;

        static UtilUi()
        {
            Canvas2 = FindObjectOfType<Canvas>(GameLayer.UI);
            Canvas3 = FindObjectOfType<Canvas>(GameLayer.UI3D);

            UtilAssert.IsNotNull(Canvas2, "场景中没有找到2D Canvas");
            UtilAssert.IsNotNull(Canvas3, "场景中没有找到3D Canvas");

            foreach (UiLayer it in Enum.GetValues(typeof(UiLayer)))
            {
                var layer = new GameObject("Layer." + it, typeof(RectTransform));
                layer.transform.SetParent(Canvas2.transform);
                layer.transform.SetAsStretch();
                layer.layer = (int)GameLayer.UI;
                mLayers.Add(it, layer.transform as RectTransform);
            }
        }
        internal static RectTransform GetLayer(UiLayer layer)
        {
            return mLayers[layer];
        }
        public static T CreateUi<T>(string ui_name) where T : Ui, new()
        {
            T ui = new T();
            ui.Init(GetPrefab(ui_name));
            return ui;
        }
        public static T CreateUi<T>(GameObject prefab) where T : Ui, new()
        {
            T ui = new T();
            ui.Init(prefab);
            ui.SetLayer(UiLayer.Fg);
            return ui;
        }
        public static T CreateUi<T>(string ui_name, UiLayer layer) where T : Ui, new()
        {
            T ui = new T();
            ui.Init(GetPrefab(ui_name));
            ui.SetLayer(layer);
            return ui;
        }
        public static T CreateUi<T, R>(R refer) 
            where T : Ui<R>, new()
            where R : Refer
        {
            T ui = new T();
            ui.Init(refer.gameObject);
            return ui;
        }
        public static T CreateUi3<T>(string ui_name) where T : Ui3, new()
        {
            T ui = new T();
            ui.Init(GetPrefab2(ui_name, Canvas3.transform));
            ui.SetParent(Canvas3.transform as RectTransform);
            return ui;
        }

        static GameObject GetPrefab(string ui_name)
        {
            return GetPrefab(ui_name, Canvas2.transform);
        }
        static GameObject GetPrefab(string ui_name, Transform from)
        {
            Transform transform = from.FindChild(ui_name);
            GameObject prefab = null;
            if (transform == null)
            {
                prefab = UtilResource.LoadPrefab(UtilPath.Combine("UI", ui_name));
                prefab.transform.SetParent(from, false);
            }
            else
            {
                prefab = transform.gameObject;
            }

            return prefab;
        }
        static GameObject GetPrefab2(string ui_name, Transform from)
        {
            GameObject prefab = null;
            prefab = UtilResource.LoadPrefab(UtilPath.Combine("Ui", ui_name));
            prefab.transform.SetParent(from, false);
            return prefab;
        }

        static T FindObjectOfType<T>(GameLayer layer) where T : Component
        {
            foreach (var it in GameObject.FindObjectsOfType<T>())
            {
                if (it.gameObject.layer == (int)layer)
                {
                    return it;
                }
            }
            return null;
        }
    }
}
