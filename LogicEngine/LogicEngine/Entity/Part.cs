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
    public abstract class Part : Entity.IPart
    {
        public Entity entity { get; private set; }
        public bool IsRelease { get { return entity.mParent == null; } }
        ICollection<Action> mObservers = new MutableCollection<Action>();

        void Entity.IPart.Awake(Entity entity)
        {
            this.entity = entity;
            _Awake0();
            _Awake();
        }
        void Entity.IPart.Release()
        {
            _Release();
            _Release0();
        }
        public void Attach(Action on_change)
        {
            mObservers.Add(on_change);
            on_change();
        }
        public void Detach(Action on_change)
        {
            mObservers.Remove(on_change);
        }
        protected void Notify()
        {
            foreach (var it in mObservers)
            {
                it();
            }
        }
        protected Entity AddChild() { return entity.AddChild(); }
        protected T GetPart<T>() where T : class, Entity.IPart, new() { return entity.GetPart<T>(); }
        protected T GetPartInParent<T>() where T : class, Entity.IPart, new() { return entity.GetPartInParent<T>(); }
        protected T AddPart<T>() where T : class, Entity.IPart, new() { return entity.AddPart<T>(); }

        internal virtual void _Awake0() { }
        internal virtual void _Release0() { }
        protected abstract void _Awake();
        protected abstract void _Release();
    }
    public abstract class UPart : Part
    {
        public bool enableUpdate
        {
            get
            {
                return mEnableUpdate;
            }
            set
            {
                if (mEnableUpdate == value) return;
                mEnableUpdate = value;
                if (mEnableUpdate)
                {
                    Logic.AddUpdate(Update);
                }
                else
                {
                    Logic.RemoveUpdate(Update);
                }
            }
        }
        bool mEnableUpdate;
        internal override void _Awake0()
        {
            base._Awake0();
            enableUpdate = true;
        }
        internal override void _Release0()
        {
            enableUpdate = false;
            base._Release0();
        }
        void Update(float sec)
        {
            _Update(sec);
        }
        protected abstract void _Update(float sec);
    }
    public abstract class Part<TDyc> : Part, IDycPart<TDyc>, ICmdPart where TDyc : Dyc
    {
        public long id { get { return dyc.id; } }
        TDyc dyc;
        CmdRoute route;

        internal override void _Awake0()
        {
            base._Awake0();
            route = GetPartInParent<CmdRoute>();
        }
        internal override void _Release0()
        {
            if (route != null)
            {
                route.Detach(this);
            }
        }
        public void Sync(TDyc dyc)
        {
            if (dyc == null) return;
            this.dyc = dyc;
            if (route != null)
            {
                route.Attach(this);
            }
            _Sync(dyc);
            Notify();
        }
        public TDyc Save()
        {
            _Save(dyc);
            return dyc;
        }
        protected TCmd GetCmd<TCmd, TCmdPart>()
            where TCmd : Cmd<TCmdPart>, new()
            where TCmdPart : Part<TDyc>
        {
            if (route == null)
            {
                UtilLog.LogError("没有CmdRoute部件");
            }
            else
            {
                var part = this as TCmdPart;
                if (part == null)
                {
                    UtilLog.LogError("获取的命令类型不对，参数TDycPart必须是该类子类");
                }
                else
                {
                    return route.GetCmd<TCmd, TCmdPart>(part);
                }
            }
            return null;
        }
        protected abstract void _Sync(TDyc dyc);
        protected abstract void _Save(TDyc dyc);
    }
    public abstract class CPart<TPart> : Entity.IPart, IScenePart
        where TPart : global::LogicEngine.Part, new()
    {
        protected TPart Part { get; private set; }
        SceneMgr sceneMgr;
        void Entity.IPart.Awake(Entity entity)
        {
            Part = entity.GetPart<TPart>();
            UtilAssert.IsNotNull(Part, "没有" + typeof(TPart).Name);
            sceneMgr = entity.GetPartInParent<SceneMgr>();
            sceneMgr.currentScene.AddPart(this);
            _Awake();
        }
        void Entity.IPart.Release()
        {
            _Release();
            sceneMgr.currentScene.RemovPart(this);
        }
        void IScenePart.OnShow()
        {
            Part.Attach(OnChange);
        }
        void IScenePart.OnHide()
        {
            Part.Detach(OnChange);
        }
        void OnChange()
        {
            _OnChange();
        }
        protected abstract void _Awake();
        protected abstract void _Release();
        protected abstract void _OnChange();
    }
}