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
    public abstract class Center<TCenter> : Module<TCenter>
        where TCenter : Center<TCenter>, new()
    {
        Dictionary<long, IPeer> mPeers = new Dictionary<long, IPeer>();

        internal override void _Awake0()
        {
            base._Awake0();
        }
        internal override void _Release0()
        {
        }

        public void AddRequest<TRequest>(Action<TRequest> fun)
             where TRequest : Request
        {
        }

        public void AddPeer(IPeer peer)
        {
            //mPeers.Add(other_peer_id, peer);
        }
        
        void OnRequest(Request request)
        {
            string error = string.Empty;
            if (request.TryVerify(ref error))
            {
            }
            else
            {
                var response = request.inCreateResponseError("请求{"+ request.GetType().Name + "}出错校验失败:"+ (string.IsNullOrEmpty(error) ? "未知" : error));

            }
        }

        public abstract class User : LogicEngine.Part
        {
            IPeer mPeer;

            public void SetPeer(IPeer peer)
            {
                mPeer = peer;
            }

            public TRequest GetRequest<TRequest>(long other_peer_id)
            where TRequest : Request, new()
            {
                IPeer peer;
                //if (mPeers.TryGetValue(other_peer_id, out peer))
                //{
                //    var request = new TRequest();
                //    request.peer = peer;
                //    return request;
                //}
                throw new NullReferenceException("创建Request错误");
            }
        }
    }
}