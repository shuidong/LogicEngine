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
using LogicEngine;
using LogicEngine.VirtualNet;

namespace Demos.BoomBeach.Server
{
    public static class Urls
    {
        public static readonly Url ChatCenter = new Url(0);
        public static readonly Url GameCenter = new Url(1);
    }
    public class ManagerCenter : Center<ManagerCenter>
    {
        public Device NetDevice { get; private set; }
        public ChatCenter ChatCenter { get; private set; }
        public GameCenter GameCenter { get; private set; }
        
        protected override void _Awake()
        {
            NetDevice = AddChild().AddPart<Device>();

            ChatCenter = AddChild().AddPart<ChatCenter>();
            GameCenter = AddChild().AddPart<GameCenter>();

            ChatCenter.AddPeer(NetDevice.AddPeer(Urls.ChatCenter));
            GameCenter.AddPeer(NetDevice.AddPeer(Urls.GameCenter));
        }
        protected override void _Release()
        {
        }
    }
}