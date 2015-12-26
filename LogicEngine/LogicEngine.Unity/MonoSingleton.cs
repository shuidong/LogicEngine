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
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        static T singleton;

        public static T Instance
        {
            get
            {
                if (singleton == null)
                {
                    UtilGameObject.Singleton.AddComponent<T>();
                }
                return singleton;
            }
        }

        internal virtual void Awake()
        {
            if (singleton == null)
            {
                singleton = this as T;
                _Init();
            }
            else
            {
                Debug.LogWarning("已经存在实例:" + typeof(T).Name);
                GameObject.Destroy(this);
            }
        }

        protected abstract void _Init();
    }
}
