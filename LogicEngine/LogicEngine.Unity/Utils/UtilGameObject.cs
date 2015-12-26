//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;

namespace LogicEngine.Unity
{
    public static class UtilGameObject
    {
        internal static GameObject Singleton;
        static UtilGameObject()
        {
            Singleton = new GameObject("Singleton");
            GameObject.DontDestroyOnLoad(Singleton);
        }

        public static GameObject Create()
        {
            return Create("<None>");
        }

        public static GameObject Create(string name)
        {
            return new GameObject(name);
        }

        public static void Destroy(GameObject game_object)
        {
            GameObject.Destroy(game_object);
        }

        public static T GetOrAddComponent<T>(this GameObject game_object) where T : Component
        {
            if (game_object == null)
            {
                return null;
            }
            T component = game_object.GetComponent<T>();
            if (component == null)
            {
                component = game_object.AddComponent<T>();
            }
            return component;
        }
    }
}
