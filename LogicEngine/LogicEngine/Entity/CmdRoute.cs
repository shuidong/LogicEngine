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
    class CmdRoute : Part, IPeerListener
    {
        Dictionary<long, ICmdPart> mParts = new Dictionary<long, ICmdPart>();
        Table mTypeTable = new Table();
        IPeer mPeer;

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        public void AddCmd<TCmd, TCmdPart>()
            where TCmd : Cmd<TCmdPart>, new()
            where TCmdPart : class, ICmdPart
        {
            mTypeTable.Add(typeof(TCmd));
        }
        public void SetPeer(IPeer peer)
        {
            mPeer = peer;
            mPeer.SetListener(this);
        }

        internal void Attach(ICmdPart part)
        {
            if (mParts.ContainsKey(part.id))
            {
                mParts[part.id] = part;
            }
            else 
            {
                mParts.Add(part.id, part);
            }
        }
        internal void Detach(ICmdPart part)
        {
            mParts.Remove(part.id);
        }
        internal TCmd GetCmd<TCmd, TCmdPart>(TCmdPart part)
            where TCmd : Cmd<TCmdPart>, new()
            where TCmdPart : class, ICmdPart
        {
            var cmd = new TCmd();
            (cmd as ICmd).SetPart(part);
            cmd.cmdRoute = this;
            return cmd;
        }
        internal void SendCmd(ICmd cmd)
        {
            if (mPeer == null)
            {
                UtilLog.LogWarning("CmdRoute's Peer未设置");
            }
            else
            {
                var cmd_type_id = mTypeTable.GetID(cmd.GetType());
                if (cmd_type_id == Table.invalidID)
                {
                    UtilLog.LogWarning("Cmd[" + cmd.GetType().Name + "]未注册");
                }
                else
                {
                    //mPeer.SendCmd(cmd_type_id, cmd);
                    throw new NotImplementedException();
                }
            }
        }
        void IPeerListener.OnJson(Url url, string json)
        {
            throw new NotImplementedException();
        }
        void IPeerListener.OnCmd(long cmd_type_id, string json)
        {
            var type = mTypeTable.GetType(cmd_type_id);
            if (type == null)
            {
                UtilLog.LogError("RouteTable:查询不到id[" + cmd_type_id + "]的Cmd类型");
            }
            else
            {
                var cmd = UtilJson.To(json, type) as ICmd;
                if (cmd == null)
                {
                    UtilLog.LogError("RouteTable:Cmd[" + (json == null ? "<none>" : json) + "]反序列化失败");
                }
                else
                {
                    ICmdPart part;
                    if (mParts.TryGetValue(cmd.GetID(), out part))
                    {
                        cmd.SetPart(part);
                        (cmd as ILocalCmd).Execute();
                    }
                    else 
                    {
                        UtilLog.LogError("RouteTable:ICmdPart[" + cmd.GetID() + "]not found");
                    }
                }
            }
        }

        class Table
        {
            public static readonly long invalidID = -1;
            long mID;
            Dictionary<long, Type> mTypes = new Dictionary<long, Type>();
            Dictionary<Type, long> mTypes2 = new Dictionary<Type, long>();

            public void Add(Type type)
            {
                mTypes.Add(mID, type);
                mTypes2.Add(type, mID);
                mID++;
            }
            public Type GetType(long id)
            {
                Type type;
                if (mTypes.TryGetValue(id, out type))
                {
                    return type;
                }
                return null;
            }
            public long GetID(Type type)
            {
                long id;
                if (mTypes2.TryGetValue(type, out id))
                {
                    return id;
                }
                return invalidID;
            }
        }
    }
}