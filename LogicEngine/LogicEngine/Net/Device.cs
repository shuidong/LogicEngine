//======================================================
// Create by @Peng Guang Hui
// 2015/11/30 13:11:54
//======================================================
using System;
using System.Collections.Generic;

namespace LogicEngine.VirtualNet
{
    public sealed class Device : UPart
    {
        Dictionary<long, IPeer> mPeers = new Dictionary<long, IPeer>();
        Dictionary<IPeer, List<string>> mPackets = new Dictionary<IPeer, List<string>>();

        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        protected override void _Update(float sec)
        {
            foreach (var it in mPackets)
            {
                Peer peer = it.Key as Peer;
                foreach (var packet in it.Value)
                {
                    peer.OnJson(packet);
                }
                it.Value.Clear();
            }
        }
        public IPeer AddPeer(Url url)
        {
            var peer = new Peer(url, this);
            mPeers.Add(url.ID, peer);
            mPackets.Add(peer, new List<string>());
            return peer;
        }
        internal void SendJson(Url url, string json)
        {
            IPeer peer;
            if (mPeers.TryGetValue(url.ID, out peer))
            {
                List<string> packets;
                if (mPackets.TryGetValue(peer, out packets))
                {
                    packets.Add(json);
                }
            }
        }
    }
}