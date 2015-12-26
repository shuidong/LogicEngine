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
using System.Text;

namespace LogicEngine
{
    public class Entity
    {
        internal Entity mParent;
        internal List<Entity> mChildren = new List<Entity>();
        internal Dictionary<Type, IPart> mParts = new Dictionary<Type, IPart>();

        public Entity AddChild()
        {
            Entity child = new Entity();
            child.mParent = this;
            this.mChildren.Add(child);
            return child;
        }
        public void Release()
        {
            if (mParent == null) return;
            mParent.mChildren.Remove(this);
            _Release();
        }
        void _Release()
        {
            for (int i = mChildren.Count - 1; i >= 0; i--)
            {
                mChildren[i]._Release();
            }
            foreach (var it in mParts)
            {
                it.Value.Release();
            }
            mParts.Clear();
            mParent = null;
        }
        public IPart AddPart(Type type)
        {
            throw new NotImplementedException();
        }
        public T AddPart<T>() where T : class, IPart, new()
        {
            var key = typeof(T);
            foreach (RequirePartAttribute it in Attribute.GetCustomAttributes(key, inConstants.RequirePartAttribute))
            {
                if (IsLackOfPart(it.RequirePart))
                {
                    UtilLog.LogError(string.Format("Adding {0} part failed. Add required part of type '{1}' to the entity first.", key.Name, it.RequirePart.Name));
                    return null;
                }
            }
            foreach (RequirePartInParentAttribute it in Attribute.GetCustomAttributes(key, inConstants.RequirePartInParentAttribute))
            {
                if (IsLackOfPartInParent(it.RequirePart))
                {
                    UtilLog.LogError(string.Format("Adding {0} part failed. Add required part type '{1}' to the entity '\'s parent first.", key.Name, it.RequirePart.Name));
                    return null;
                }
            }
            IPart ipart;
            if (mParts.TryGetValue(key, out ipart))
            {
                throw new Exception("Entiy can't add same Part");
            }
            T part = new T();
            mParts.Add(key, part);
            part.Awake(this);
            return part;
        }
        bool IsLackOfPart(Type part_type)
        {
            return !mParts.ContainsKey(part_type);
        }
        bool IsLackOfPartInParent(Type part_type)
        {
            Entity target = this;
            do
            {
                if (target.IsLackOfPart(part_type))
                {
                    target = target.mParent;
                }
                else
                {
                    return false;
                }
            } while (target != null);
            return true;
        }
        public T GetPart<T>() where T : class, IPart, new()
        {
            IPart part;
            if (mParts.TryGetValue(typeof(T), out part))
            {
                return part as T;
            }
            return null;
        }
        public T GetOrAddPart<T>() where T : class, IPart, new()
        {
            IPart part;
            if (mParts.TryGetValue(typeof(T), out part))
            {
                return part as T;
            }
            else
            {
                return AddPart<T>();
            }
        }
        internal T GetPartInParent<T>() where T : class, IPart, new()
        {
            Entity target = this;
            do
            {
                var part = target.GetPart<T>();
                if (part == null)
                {
                    target = target.mParent;
                }
                else
                {
                    return part;
                }
            } while (target != null);
            return null;
        }
        public bool HasPart<T>() where T : IPart, new()
        {
            return mParts.ContainsKey(typeof(T));
        }
        public void RemovePart<T>() where T : class, IPart, new()
        {
            var key = typeof(T);
            IPart part;
            if (mParts.TryGetValue(key, out part))
            {
                part.Release();
                mParts.Remove(key);
            }
        }
        public interface IPart
        {
            void Awake(Entity entity);
            void Release();
        }

        public string ToTree()
        {
            return ToTree(0, this);
        }
        static string ToTree(int gap_count, Entity entity)
        {
            string s = Pattern(gap_count, "----", "--->") + "[" + GetParts(entity) + "]\n";
            foreach (var it in entity.mChildren)
            {
                s += ToTree(gap_count + 1, it);
            }
            return s;
        }
        static string GetParts(Entity entity)
        {
            StringBuilder result = new StringBuilder();
            int index = 0;
            foreach (var it in entity.mParts)
            {
                result.Append(it.Key.Name + (index == entity.mParts.Count - 1 ? "" : ","));
                index++;
            }
            return result.ToString();
        }
        static string Pattern(int count, params string[] patterns)
        {
            if (patterns.Length == 0)
            {
                throw new Exception("must have pattern");
            }
            StringBuilder result = new StringBuilder();
            int diff = count - patterns.Length;
            for (int i = 0; i < count; ++i)
            {
                int index = i - diff;
                if (index < 0) index = 0;
                result.Append(patterns[index]);
            }
            return result.ToString();
        }

        #region 待整理
        internal T GetPartByBase<T>() where T : class, IPart
        {
            var b = typeof(T);
            var entity = this;
            while (entity != null)
            {
                foreach (var it in mParts)
                {
                    if (b.IsAssignableFrom(it.Value.GetType()))
                    {
                        return (T)it.Value;
                    }
                }
                entity = entity.mParent;
            }
            return null;
        }
        internal void RemovePartWithoutRelease(IPart part)
        {
            var key = part.GetType();
            if (mParts.TryGetValue(key, out part))
            {
                mParts.Remove(key);
            }
        }
        internal void RemovePart(Type key)
        {
            IPart part;
            if (mParts.TryGetValue(key, out part))
            {
                part.Release();
                mParts.Remove(key);
            }
        }
        /// <summary>
        /// 此方法比较耗时，因为需要遍历所有的子节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messgae"></param>
        /// <param name="args"></param>
        public void SendAllMessage<T>(string messgae, params object[] args)
            where T : IPart
        {
            _SendAllMessage<T>(typeof(T), messgae, args);
        }
        void _SendAllMessage<T>(Type super, string messgae, object[] args)
            where T : IPart
        {
            if (mChildren.Count > 0)
            {
                foreach (var it in mChildren)
                {
                    it._SendAllMessage<T>(super, messgae, args);
                }
            }
            foreach (var it in mParts)
            {
                if (it.Key.IsSubclassOf(super))
                {
                    UtilMessage.SendMessage(super, it.Value, messgae, args);
                }
            }
        }
        internal void ReleaseAllPart<T>()
        {
            _ReleaseAllPart(typeof(T));
        }
        void _ReleaseAllPart(Type type)
        {
            if (mChildren.Count > 0)
            {
                foreach (var it in mChildren)
                {
                    it._ReleaseAllPart(type);
                }
            }
            inConstants.ForClear.Clear();
            foreach (var it in mParts)
            {
                if (it.Key.IsSubclassOf(type))
                {
                    inConstants.ForClear.Add(it.Key);
                }
            }
            foreach (var it in inConstants.ForClear)
            {
                IPart part;
                if (mParts.TryGetValue(it, out part))
                {
                    part.Release();
                    mParts.Remove(it);
                }
            }
        }
        internal static void ReleaseALL(Entity root)
        {
            var all = root.mChildren.FindAll(it => true);
            foreach (var it in all)
            {
                it.Release();
            }
            root.Release();
        }
        #endregion
    }
}