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
using Mono.Xml;
using System.Security;


namespace LogicEngine
{
    /// <summary>
    /// Static Data
    /// </summary>
    public class Cfg
    {
        public readonly string name;
        public readonly string id;

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class MetaAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
        public class RangeAttribute : Attribute
        {
            public float Min { get; private set; }
            public float Max { get; private set; }
            public RangeAttribute(float min, float max)
            {
                Min = min;
                Max = max;
            }
            public RangeAttribute(int min, int max)
            {
                Min = min;
                Max = max;
            }
        }
    }

    public sealed class CfgSet<T> : IEnumerable<T>
        where T : Cfg, new()
    {
        Dictionary<string, T> mCfgs = new Dictionary<string, T>();

        public int Count { get { return mCfgs.Count; } }

        public bool Contains(string id)
        {
            return mCfgs.ContainsKey(id);
        }

        public T Get(string id)
        {
            T v = default(T);
            if (!mCfgs.TryGetValue(id, out v))
            {
                UtilLog.LogWarning("数据集没有id:" + id + "的数据");
            }
            return v;
        }
        public void Foreach(Action<T> fun)
        {
            foreach (var it in mCfgs)
            {
                fun(it.Value);
            }
        }
        public CfgSet<T> All(Predicate<T> match)
        {
            CfgSet<T> set = new CfgSet<T>();
            foreach (var it in mCfgs)
            {
                if (match(it.Value))
                {
                    set.Add(it.Key, it.Value);
                }
            }
            return set;
        }
        public T Any(Predicate<T> match)
        {
            foreach (var it in mCfgs)
            {
                if (match(it.Value))
                {
                    return it.Value;
                }
            }
            return default(T);
        }
        internal void Add(string id, T v)
        {
            mCfgs.Add(id, v);
        }
        public List<T> ToList()
        {
            List<T> list = new List<T>(mCfgs.Count);
            foreach (var it in mCfgs)
            {
                list.Add(it.Value);
            }
            return list;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(mCfgs);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(mCfgs);
        }
        public struct Enumerator : IEnumerator<T>
        {
            Dictionary<string, T>.Enumerator mEnumerator;
            internal Enumerator(Dictionary<string, T> values)
            {
                mEnumerator = values.GetEnumerator();
            }
            T IEnumerator<T>.Current
            {
                get { return mEnumerator.Current.Value; }
            }
            void IDisposable.Dispose()
            {
                mEnumerator.Dispose();
            }
            object IEnumerator.Current
            {
                get { return mEnumerator.Current.Value; }
            }
            bool IEnumerator.MoveNext()
            {
                return mEnumerator.MoveNext();
            }
            void IEnumerator.Reset()
            {
                (mEnumerator as IEnumerator).Reset();
            }
        }
    }
}