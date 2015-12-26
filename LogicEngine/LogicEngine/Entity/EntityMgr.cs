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
    public class EntityMgr<TPart, TDyc> : Entity.IPart, IEnumerable<TPart>
        where TPart : Part, IDycPart<TDyc>, new()
        where TDyc : Dyc
    {
        Entity entity;
        Dictionary<long, TPart> mValues = new Dictionary<long, TPart>();
        List<Action<TPart>> mObservers = new List<Action<TPart>>();
        static Dictionary<long, TPart> mForSync = new Dictionary<long, TPart>();

        void Entity.IPart.Awake(Entity entity)
        {
            this.entity = entity;
            _Awake();
        }
        void Entity.IPart.Release()
        {
            _Release();
        }
        protected virtual void _Awake() { }
        protected virtual void _Release() { }

        #region crud
        public void Sync(List<TDyc> dycs)
        {
            foreach (var it in mValues)
            {
                mForSync.Add(it.Key, it.Value);
            }
            mValues.Clear();
            foreach (var it in dycs)
            {
                TPart part;
                if (mForSync.TryGetValue(it.id, out part))
                {
                    part.Sync(it);
                    mValues.Add(it.id, part);
                    mForSync.Remove(it.id);
                }
                else
                {
                    part = AddNew(it);
                }
            }
            foreach (var it in mForSync)
            {
                it.Value.entity.Release();
            }
            mForSync.Clear();
        }
        public TPart Add(TDyc dyc, bool revise = false)
        {
            UtilAssert.IsNotNull(dyc, "参数[" + typeof(TDyc).Name + "] is null");

            if (revise && !dyc.TryRevise())
            {
                return null;
            }

            TPart part;
            if (mValues.TryGetValue(dyc.id, out part))
            {
                part.Sync(dyc);
            }
            else
            {
                part = AddNew(dyc);
            }
            return part;
        }
        TPart AddNew(TDyc dyc)
        {
            TPart part = entity.AddChild().AddPart<TPart>();
            part.Sync(dyc);
            mValues.Add(dyc.id, part);
            foreach (var it in mObservers)
            {
                try
                {
                    it(part);
                }
                catch (Exception e)
                {
                    UtilLog.LogError(e.ToString());
                }
            }
            return part;
        }
        public void Remove(TPart item)
        {
            mValues.Remove(item.id);
            item.entity.Release();
        }
        public void Clear()
        {
            foreach (var it in mValues)
            {
                it.Value.entity.Release();
            }
            mValues.Clear();
        }
        public int Count
        {
            get { return mValues.Count; }
        }
        public bool Contains(long id)
        {
            return mValues.ContainsKey(id);
        }
        public bool Contains(TPart item)
        {
            return mValues.ContainsKey(item.id);
        }
        public TPart Find(long id)
        {
            TPart part;
            if (mValues.TryGetValue(id, out part))
            {
                return part;
            }
            return null;
        }
        public TPart Find(Predicate<TPart> match)
        {
            foreach (var it in mValues)
            {
                if (match(it.Value))
                {
                    return it.Value;
                }
            }
            return null;
        }
        public List<TPart> FindAll(Predicate<TPart> match)
        {
            List<TPart> all = new List<TPart>();
            foreach (var it in mValues)
            {
                if (match(it.Value))
                {
                    all.Add(it.Value);
                }
            }
            return all;
        }
        public void Foreach(Action<TPart> fun)
        {
            foreach (var it in mValues)
            {
                fun(it.Value);
            }
        }
        IEnumerator<TPart> IEnumerable<TPart>.GetEnumerator()
        {
            return new Enumerator(mValues);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(mValues);
        }
        public List<R> Map<R>(Func<TPart, R> fun)
        {
            return mValues.MapValue(fun);
        }
        #endregion

        public void Attach(Action<TPart> observer, bool needAdd = true)
        {
            if (observer != null)
            {
                if (!mObservers.Contains(observer))
                {
                    mObservers.Add(observer);
                }
                if (needAdd)
                {
                    foreach (var it in mValues)
                    {
                        try
                        {
                            observer(it.Value);
                        }
                        catch (Exception e)
                        {
                            UtilLog.LogError(e.ToString());
                        }
                    }
                }
            }
        }
        public void Detach(Action<TPart> observer)
        {
            if (observer != null)
            {
                mObservers.Remove(observer);
            }
        }
        public void SaveTo(ref List<TDyc> list)
        {
            list.Clear();
            foreach (var it in mValues)
            {
                list.Add(it.Value.Save());
            }
        }

        public struct Enumerator : IEnumerator<TPart>
        {
            Dictionary<long, TPart>.Enumerator mEnumerator;
            internal Enumerator(Dictionary<long, TPart> values)
            {
                mEnumerator = values.GetEnumerator();
            }
            TPart IEnumerator<TPart>.Current
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

    public class EntityMgr<TPart> : Entity.IPart, IEnumerable<TPart>
        where TPart : Part, new()
    {
        Entity entity;
        List<TPart> mValues = new List<TPart>();
        List<Action<TPart>> mObservers = new List<Action<TPart>>();

        void Entity.IPart.Awake(Entity entity)
        {
            this.entity = entity;
            _Awake();
        }
        void Entity.IPart.Release()
        {
            _Release();
        }
        protected virtual void _Awake() { }
        protected virtual void _Release() { }

        #region crud
        public TPart Add()
        {
            var part = entity.AddChild().AddPart<TPart>();
            mValues.Add(part);
            return part;
        }
        public void Remove(TPart item)
        {
            mValues.Remove(item);
            item.entity.Release();
        }
        public void Clear()
        {
            foreach (var it in mValues)
            {
                it.entity.Release();
            }
            mValues.Clear();
        }
        public int Count
        {
            get { return mValues.Count; }
        }
        public bool Contains(TPart item)
        {
            return mValues.Contains(item);
        }
        public TPart Find(Predicate<TPart> match)
        {
            foreach (var it in mValues)
            {
                if (match(it))
                {
                    return it;
                }
            }
            return null;
        }
        public List<TPart> FindAll(Predicate<TPart> match)
        {
            List<TPart> all = new List<TPart>();
            foreach (var it in mValues)
            {
                if (match(it))
                {
                    all.Add(it);
                }
            }
            return all;
        }
        public void Foreach(Action<TPart> fun)
        {
            foreach (var it in mValues)
            {
                fun(it);
            }
        }
        IEnumerator<TPart> IEnumerable<TPart>.GetEnumerator()
        {
            return mValues.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mValues.GetEnumerator();
        }
        public List<R> Map<R>(Func<TPart, R> fun)
        {
            return mValues.Map(fun);
        }
        #endregion

        public void Attach(Action<TPart> observer, bool needAdd = true)
        {
            if (observer != null)
            {
                if (!mObservers.Contains(observer))
                {
                    mObservers.Add(observer);
                }
                if (needAdd)
                {
                    foreach (var it in mValues)
                    {
                        try
                        {
                            observer(it);
                        }
                        catch (Exception e)
                        {
                            UtilLog.LogError(e.ToString());
                        }
                    }
                }
            }
        }
        public void Detach(Action<TPart> observer)
        {
            if (observer != null)
            {
                mObservers.Remove(observer);
            }
        }
    }
}