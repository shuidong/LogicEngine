//======================================================
// Create by @Peng Guang Hui
// 2015/12/2 11:52:04
//======================================================
using System;
using System.Collections.Generic;

namespace LogicEngine
{
    public abstract class Play<TPlay, TDyc, TMessage, TRpc> : Module<TPlay>, IDycPart<TDyc>, ICmdPart
        where TPlay : Play<TPlay, TDyc, TMessage, TRpc>, new()
        where TDyc : Dyc
        where TMessage : struct, IConvertible
        where TRpc : struct, IConvertible
    {
        public long id { get { return dyc.id; } }
        public bool acceptSync { get; set; }
        CmdRoute route;
        Dictionary<long, Action<object[]>> rpcTable = new Dictionary<long, Action<object[]>>();
        TDyc dyc;

        internal override void _Awake0()
        {
            base._Awake0();
            route = AddPart<CmdRoute>();
            route.AddCmd<CmdRpc, Play<TPlay, TDyc, TMessage, TRpc>>();
            route.AddCmd<CmdAskSync, Play<TPlay, TDyc, TMessage, TRpc>>();
            route.AddCmd<CmdSync, Play<TPlay, TDyc, TMessage, TRpc>>();
        }
        internal override void _Release0()
        {
            if (route != null)
            {
                route.Detach(this);
            }
        }
        public void AddCmd<TCmd, TCmdPart>()
            where TCmd : Cmd<TCmdPart>, new()
            where TCmdPart : class, ICmdPart
        {
            route.AddCmd<TCmd, TCmdPart>();
        }
        public void AddRpc(TRpc rpc, Action<object[]> fun)
        {
            UtilLog.LogWarning(rpc.ToInt64(null) + GetType().Name);
            rpcTable.Add(rpc.ToInt64(null), fun);
        }
        public void SetPeer(IPeer peer, Url url_mirror)
        {
            route.SetPeer(peer);
        }
        public void Sync(TDyc dyc)
        {
            if (dyc == null) return;
            this.dyc = dyc;
            if (route != null)
            {
                route.Attach(this);
            }
            _Load(dyc);
            Notify();
        }
        public TDyc Save()
        {
            _Save(dyc);
            return dyc;
        }
        protected abstract void _Load(TDyc dyc);
        protected abstract void _Save(TDyc dyc);
        protected TCmd GetCmd<TCmd, TCmdPart>()
            where TCmd : Cmd<TCmdPart>, new()
            where TCmdPart : Play<TPlay, TDyc, TMessage, TRpc>
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
        public void Rpc(TRpc rpc, params object[] args)
        {
            var cmd = GetCmd<CmdRpc, Play<TPlay, TDyc, TMessage, TRpc>>();
            cmd.d0 = rpc.ToInt64(null);
            cmd.d1 = args;
            cmd.Execute();
        }
        public void Sync()
        {
            var cmd = GetCmd<CmdSync, Play<TPlay, TDyc, TMessage, TRpc>>();
            cmd.d0 = Save();
            cmd.Execute();
        }
        public void AskSync()
        {
            if (acceptSync)
            {
                var cmd = GetCmd<CmdAskSync, Play<TPlay, TDyc, TMessage, TRpc>>();
                cmd.Execute();
            }
        }
        internal void OnRpc(long id, object[] args)
        {
            Action<object[]> fun;
            if (rpcTable.TryGetValue(id, out fun))
            {
                try
                {
                    fun(args);
                }
                catch (Exception e)
                {
                    UtilLog.LogError(e.ToString());
                }
            }
            else
            {
                var ss = Enum.Parse(typeof(TRpc), id.ToString());
                UtilLog.LogError(id + "未注册的远程调用:" + rpcTable.Count);
            }
        }

        [CmdPost(CmdMode.Target)]
        class CmdRpc : Cmd<Play<TPlay, TDyc, TMessage, TRpc>>
        {
            public long d0;
            public object[] d1;
            protected override void _Execute(Play<TPlay, TDyc, TMessage, TRpc> part)
            {
                part.OnRpc(d0, d1);
            }
        }
        [CmdPost(CmdMode.Target)]
        class CmdAskSync : Cmd<Play<TPlay, TDyc, TMessage, TRpc>>
        {
            protected override void _Execute(Play<TPlay, TDyc, TMessage, TRpc> part)
            {
                part.Sync();
            }
        }
        [CmdPost(CmdMode.Target)]
        class CmdSync : Cmd<Play<TPlay, TDyc, TMessage, TRpc>>
        {
            public TDyc d0;
            protected override void _Execute(Play<TPlay, TDyc, TMessage, TRpc> part)
            {
                if (part.acceptSync)
                {
                    part.Sync(d0);
                }
            }
        }
    }
}