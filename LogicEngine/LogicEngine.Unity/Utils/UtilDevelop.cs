//======================================================
// Create by @Peng Guang Hui
// 2015/10/21 15:47:34
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace LogicEngine.Unity
{
    public static class UtilDevelop
    {
        static GameObject mRoot;
        static Dictionary<string, GameObject> mCategories = new Dictionary<string, GameObject>();
        static UiGraphicLog mUiGraphicLog;

        static UtilDevelop()
        {
            mRoot = new GameObject("Develop");
            GameObject.DontDestroyOnLoad(mRoot);
        }

        [Conditional("DEBUG")]
        public static void Category(string names, GameObject go)
        {
            if (names == null) return;
            string[] split = names.Split('/');

            GameObject root = null;
            if (mCategories.TryGetValue(names, out root))
            {
                GameObject leaf = mRoot.transform.Find(names).gameObject;
                go.transform.SetParent(leaf.transform);
            }
            else
            {
                for (int i = 0; i < split.Length; i++)
                {
                    var name = split[i];
                    if (i == 0)
                    {
                        if (mCategories.TryGetValue(names, out root))
                        {
                        }
                        else
                        {
                            root = new GameObject(name);
                            root.transform.SetParent(mRoot.transform);
                            mCategories[names] = root;
                        }
                    }
                    else
                    {
                        var leaf = new GameObject(name);
                        leaf.transform.SetParent(root.transform);
                        root = leaf;

                    }
                }
                go.transform.SetParent(root.transform);
            }
        }
        [Conditional("DEBUG")]
        public static void LogValue(string name, float value)
        {
            if (mUiGraphicLog == null)
            {
                mUiGraphicLog = UtilUi.CreateUi<UiGraphicLog>(new GameObject("UiGraphicLog", typeof(RectTransform)));
                mUiGraphicLog.SetLayer(UiLayer.Debug);
                mUiGraphicLog.Show();
            }
            mUiGraphicLog.LogValue(name, value);
        }

         [Conditional("DEBUG")]
        public static void Run<T>(Action<T> fun)
            where T : MonoBehaviour, new()
        {
            GameObject go = new GameObject(typeof(T).Name);
            go.transform.SetParent(mRoot.transform);
            fun(go.AddComponent<T>());
        }
    }
}