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
    class Recycler : Singleton<Recycler>
    {
        Dictionary<string, Pool> mPools = new Dictionary<string, Pool>();
        Transform mPoolRoot;

        protected override void _Init()
        {
            mPoolRoot = new GameObject("RecyclablePool").transform;
            mPoolRoot.gameObject.SetActive(false);
            GameObject.DontDestroyOnLoad(mPoolRoot.gameObject);
        }

        public void Push(Recyclable recyclable)
        {
            GetPool(recyclable.mPathname).Push(mPoolRoot, recyclable);
        }
        public T Pop<T>(string pathname, Vector3 position, Quaternion rotation) where T : Recyclable
        {
            return GetPool(pathname).Pop<T>(pathname, position, rotation);
        }

        Pool GetPool(string key)
        {
            Pool pool = null;
            if (!mPools.TryGetValue(key, out pool))
            {
                pool = new Pool();
                mPools.Add(key, pool);
            }
            return pool;
        }

        class Pool
        {
            Queue<Recyclable> mRecyclables = new Queue<Recyclable>();

            GameObject mPrefab;
            Vector3 mInitPosition;
            Quaternion mInitRotation;
            Vector3 mInitScale;

            public int Count { get { return mRecyclables.Count; } }

            public void Push(Transform root, Recyclable recyclable)
            {
                recyclable.transform.SetParent(root);

                mRecyclables.Enqueue(recyclable);
                recyclable.mStage = RecycleStage.Recycled;
            }

            public T Pop<T>(string pathname, Vector3 position, Quaternion rotation) where T : Recyclable
            {
                InitPrefab(pathname);
                return _Pop<T>(pathname, position, rotation);
            }
            public T Pop<T>(string pathname) where T : Recyclable
            {
                InitPrefab(pathname);
                return _Pop<T>(pathname, mInitPosition, mInitRotation);
            }

            T _Pop<T>(string pathname, Vector3 position, Quaternion rotation) where T : Recyclable
            {
                if (Count == 0)
                {
                    var go = (GameObject.Instantiate(mPrefab, position, rotation) as GameObject);
                    T recyclable = go.GetOrAddComponent<T>();
                    recyclable.mPathname = pathname;
                    if (recyclable == null)
                    {
                        throw new System.Exception("Prefab[" + pathname + "]没有[" + typeof(T).Name + "]组件");
                    }
                    return recyclable;
                }
                else
                {
                    T recyclable = (T)mRecyclables.Dequeue();

                    recyclable.transform.SetParent(null);

                    recyclable.transform.position = position;
                    recyclable.transform.rotation = rotation;

                    //recyclable.gameObject.SetActive(false);
                    //recyclable.gameObject.SetActive(true);

                    recyclable.Awake();

                    recyclable.mStage = RecycleStage.Using;
                    return recyclable;
                }
            }
            void InitPrefab(string pathname)
            {
                if (mPrefab == null)
                {
                    mPrefab = UtilResource.Load<GameObject>(pathname);
                    mInitPosition = mPrefab.transform.position;
                    mInitRotation = mPrefab.transform.rotation;
                    mInitScale = mPrefab.transform.localScale;
                }
            }
        }
    }
}