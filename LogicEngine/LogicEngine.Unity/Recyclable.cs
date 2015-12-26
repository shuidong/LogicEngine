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

namespace LogicEngine.Unity
{
    enum RecycleStage
    {
        Using,
        WillRecycle,
        Recycled
    }
    public abstract class Recyclable : MonoBehaviour
    {
        internal string mPathname;
        internal RecycleStage mStage;

        internal void Awake()
        {
            _Awake();
        }
        protected abstract void _Awake();

        public void Release(float delay_secs = 0)
        {
            if (mStage == RecycleStage.Using)
            {
                if (delay_secs > 0)
                {
                    StartCoroutine(DelayRelease(delay_secs));
                }
                else
                {
                    _Release();
                }
            }
        }
        IEnumerator DelayRelease(float delay_secs)
        {
            mStage = RecycleStage.WillRecycle;
            yield return new WaitForSeconds(delay_secs);
            _Release();
        }
        void _Release()
        {
            if (string.IsNullOrEmpty(mPathname))
            {
#if DEBUG
                Debug.LogWarning("[" + GetType().Name + "] 未受管理，无法放入对象回收池");
                destroyByRelease = true;
#endif
                GameObject.Destroy(gameObject);
            }
            else
            {
                Recycler.Instance.Push(this);
            }
        }


#if DEBUG
        bool destroyByRelease;
#endif

        void OnDestroy()
        {
//#if DEBUG
//            if (!destroyByRelease)
//            {
//                Debug.LogWarning("[" + GetType().Name + "] 可以使用Release释放并放入到回收池里面");
//            }
//#endif
            if (mStage == RecycleStage.Recycled && Entrance.isRunning)
            {
                throw new Exception("可回收对象不可以直接销毁");
            }
        }
    }
}