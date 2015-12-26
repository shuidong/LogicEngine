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

namespace LogicEngine
{
    public class MutableCollection<T> : ICollection<T>
    {
        bool needClear;
        List<T> add = new List<T>();
        List<T> current = new List<T>();
        List<T> remove = new List<T>();

        int ICollection<T>.Count
        {
            get
            {
                return current.Count;
            }
        }
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            Sync();
            current.CopyTo(array, arrayIndex);
        }
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
        void ICollection<T>.Add(T item)
        {
            add.Add(item);
        }
        bool ICollection<T>.Remove(T item)
        {
            remove.Add(item);
            return true;
        }
        void ICollection<T>.Clear()
        {
            add.Clear();
            remove.Clear();
            needClear = true;
        }
        bool ICollection<T>.Contains(T item)
        {
            if (remove.Contains(item))
            {
                return false;
            }
            return add.Contains(item) || current.Contains(item);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Sync();
            return current.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            Sync();
            return current.GetEnumerator();
        }
        /// <summary>
        /// 将插入，删除队列放到遍历队列里面
        /// </summary>
        void Sync()
        {
            if (needClear)
            {
                current.Clear();
                needClear = false;
            }
            foreach (var it in add)
            {
                current.Add(it);
            }
            add.Clear();
            foreach (var it in remove)
            {
                current.Remove(it);
            }
            remove.Clear();
        }
    }
}