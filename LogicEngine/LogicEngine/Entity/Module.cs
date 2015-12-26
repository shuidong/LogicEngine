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
    public abstract class Module<TModule> : Part
        where TModule : Module<TModule>, new()
    {
        public abstract class Part : global::LogicEngine.Part
        {
            protected TModule Module { get; private set; }
            internal override void _Awake0()
            {
                base._Awake0();
                Module = entity.GetPartInParent<TModule>();
                UtilAssert.IsNotNull(Module, "Can't find " + typeof(TModule).Name);
            }
        }
        public abstract class Part<TDyc> : global::LogicEngine.Part<TDyc>
            where TDyc : Dyc
        {
            protected TModule Module { get; private set; }
            internal override void _Awake0()
            {
                base._Awake0();
                Module = entity.GetPartInParent<TModule>();
                UtilAssert.IsNotNull(Module, "Can't find " + typeof(TModule).Name);
            }
        }
    }

    public abstract class Module<TModule, TDyc> : Module<TModule>, IDycPart<TDyc>, ICmdPart
        where TModule : Module<TModule, TDyc>, new()
        where TDyc : Dyc
    {
        public long id { get { return dyc.id; } }
        CmdRoute route;
        TDyc dyc;

        internal override void _Awake0()
        {
            base._Awake0();
            route = GetPartInParent<CmdRoute>();
        }
        internal override void _Release0()
        {
            if (route != null && dyc != null)
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
            where TCmdPart : TModule
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
    public abstract class Module<TModule, TDyc, TMessage> : Module<TModule, TDyc>
        where TModule : Module<TModule, TDyc, TMessage>, new()
        where TDyc : Dyc
        where TMessage : struct, IConvertible
    {
        //    //Dictionary<M, ICollection<Action>> mObservers = new Dictionary<M, ICollection<Action>>();

        //    //public void Attach(M m, Object observer)
        //    //{
        //    //    //mObservers.Add(observer);
        //    //}

        //    //public void Detach(M m, Object observer)
        //    //{
        //    //    //mObservers.Remove(observer);
        //    //}

        //    //protected void Notify(M m)
        //    //{
        //    //    foreach (var it in mObservers)
        //    //    {
        //    //    }
        //    //}
    }
}