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
    public class UtilResource
    {
        internal static T Load<T>(string pathname) where T : Object
        {
            T source_object = Resources.Load<T>(pathname);
            if (source_object == null)
            {
                throw new System.Exception("没有[" + typeof(T).Name + ":Resources/" + pathname + "]资源");
            }
            return source_object;
        }
        public static T LoadRecyclable<T>(string pathname, Vector3 position, Quaternion rotation) where T : Recyclable
        {
            return Recycler.Instance.Pop<T>(pathname, position, rotation);
        }
        internal static T LoadUiPrefab<T>(string pathname) where T : Refer
        {
            return LoadRecyclable<T>(Constant.ResourcePathUi + pathname, Vector3.zero, Quaternion.identity);
        }
        public static T LoadVisibleb<T>(string pathname, Vector3 position, Quaternion rotation, string category, string name)
            where T : Visible, new()
        {
#if DEBUG
            var visible = LoadRecyclable<T>(Constant.ResourcePathVisible + pathname, position, rotation);
            UtilDevelop.Category(category, visible.gameObject);
            visible.name = name;
            return visible;
#else
            return LoadRecyclable<T>(Constant.ResourcePathVisible + pathname, position, rotation);
#endif
        }


        public static GameObject LoadPrefab(string pathname, Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate(Load<GameObject>("Prefabs/" + pathname), position, rotation) as GameObject;
        }
        //public static T LoadPrefab<T>(string pathname, Vector3 position, Quaternion rotation) where T : Visible
        //{
        //    return LoadPrefab(pathname, position, rotation).GetOrAddComponent<T>();
        //}

        public static GameObject LoadPrefab(string pathname)
        {
            return GameObject.Instantiate(Load<GameObject>("Prefabs/" + pathname)) as GameObject;
        }
        public static TextAsset LoadText(string pathname)
        {
            return Load<TextAsset>(pathname);
        }
        public static Sprite LoadSprite(string pathname)
        {
            return Load<Sprite>(pathname);
        }
    }
}