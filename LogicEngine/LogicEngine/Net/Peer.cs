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
    public class Url
    {
        internal long ID;
        public Url(long id)
        {
            ID = id;
        }
    }
    public interface IPeer
    {
        Url Url { get; }
        void SetListener(IPeerListener listener);
        void SendJson(Url url, string json);
    }
    public interface IPeerListener
    {
        void OnCmd(long id, string msg);
        void OnJson(Url url, string json);
    }
}

namespace LogicEngine.VirtualNet
{
    internal sealed class Peer : IPeer
    {
        Device mDevice;

        public Peer(Url url, Device device)
        {
        }
        internal void OnJson(string json)
        {
        }

        void IPeer.SetListener(IPeerListener listener)
        {
        }
        void IPeer.SendJson(Url url, string json)
        {
            mDevice.SendJson(url, json);
        }

        Url IPeer.Url
        {
            get { throw new NotImplementedException(); }
        }
    }
}