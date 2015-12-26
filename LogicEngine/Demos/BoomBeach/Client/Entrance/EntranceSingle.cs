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
using LogicEngine.Unity;
using LogicEngine.VirtualNet;
using UnityEngine;

using Demos.BoomBeach.Server;

namespace Demos.BoomBeach.Client
{
    public class EntranceSingle : LogicEngine.Unity.Entrance
    {
        ManagerCenter mManagerCenter;
        List<Play> mRobots = new List<Play>();
        Play mPlayer;

        protected override void _Init()
        {
            //Step 1: 服务端启动，等待客户端连接
            mManagerCenter = Logic.AddChild().AddPart<ManagerCenter>();
            AddRobots(0);

            //Step 2: 客户端初始化角色数据
            mPlayer = Logic.AddChild().AddPart<Play>();
            mPlayer.acceptSync = true;
            var device = mPlayer.entity.AddChild().AddPart<Device>();

            //Step 3: 服务端处理登录请求
            var surl = new Url(UtilRandom.GenID());
            var url = new Url(UtilRandom.GenID());

            var srole = mManagerCenter.GameCenter.Login(mManagerCenter.NetDevice.AddPeer(surl), url, UtilRandom.GenID());
            var dyc = srole.Save();

            //Step 4: 服务端数据发送给客户端
            mPlayer.Sync(dyc);
            //Step 5: 客户端连接设置
            mPlayer.SetPeer(mManagerCenter.NetDevice.AddPeer(url), surl);

            var sceneMgr = mPlayer.entity.AddPart<SceneMgr>();
            sceneMgr.Register<Scenes.SceneMainbase>("Main", mPlayer);
            sceneMgr.Switch("Main");
        }
        void AddRobots(int count)
        {
            while (count > 0)
            {
                AddRobot();
                count--;
            }
        }
        void AddRobot()
        { 
        }
    }
}