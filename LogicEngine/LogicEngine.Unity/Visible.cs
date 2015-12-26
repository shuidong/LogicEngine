//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System.Collections.Generic;
using UnityEngine;

namespace LogicEngine.Unity
{
    public abstract class Visible : Recyclable
    {
        HashSet<object> mAdd = new HashSet<object>();
        HashSet<object> mRun = new HashSet<object>();
        HashSet<object> mRemove = new HashSet<object>();

        public void AddListener(object listener)
        {
            mAdd.Add(listener);
        }
        public void RemoveListener(object listener)
        {
            mRemove.Add(listener);
        }
        protected void SendMessage(string name, params object[] args)
        {
            foreach (var it in mAdd)
            {
                mRun.Add(it);
            }
            mAdd.Clear();
            foreach (var it in mRemove)
            {
                mRun.Remove(it);
            }
            mRemove.Clear();
            UtilMessage.SendMessage(mRun, name, args);
        }
    }

    [RequireComponent(typeof(Collider))]
    public abstract class Catchable : Visible
    {
    }
    [RequireComponent(typeof(Collider2D))]
    public abstract class Catchable2D : Visible
    {
    }
    public static class UtilRaycast
    {
        public static T Catch<T>(Ray ray, params GameLayer[] layers) where T : Catchable
        {
            RaycastHit hit;
            if (layers.Length == 0)
            {
                if (Physics.Raycast(ray, out hit, float.MaxValue))
                {
                    return hit.collider.GetComponent<T>() as T;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, float.MaxValue, GetMask(layers)))
                {
                    return hit.collider.GetComponent<T>() as T;
                }
            }
            return null;
        }
        public static T Catch2D<T>(Ray ray, params GameLayer[] layers) where T : Catchable2D
        {
            //RaycastHit hit;
            //if (layers.Length == 0)
            //{
            //    if (Physics2D.(ray, out hit, float.MaxValue))
            //    {
            //        return hit.collider.GetComponent<T>() as T;
            //    }
            //}
            //else
            //{
            //    if (Physics.Raycast(ray, out hit, float.MaxValue, GetMask(layers)))
            //    {
            //        return hit.collider.GetComponent<T>() as T;
            //    }
            //}
            return null;
        }
        static LayerMask GetMask(GameLayer[] layers)
        {
            LayerMask mask = 0;
            foreach (var it in layers)
            {
                mask |= 1 << (int)(it);
            }
            return mask;
        }
    }
}