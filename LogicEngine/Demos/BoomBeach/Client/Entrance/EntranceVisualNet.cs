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
    public class EntranceVisualNet : LogicEngine.Unity.Entrance
    {
        public Device device { get; private set; }
        public GameCenter gameCenter { get; private set; }
        public Play role { get; private set; }

        protected override void _Init()
        {
            //UtilLog.LogWarning("Profiler " + Profiler.maxNumberOfSamplesPerFrame);
            //Profiler.maxNumberOfSamplesPerFrame = 8 * 1024 * 1024;

            //device = new Device();

            ////Step 1: 服务端启动，等待客户端连接
            //gameCenter = Logic.AddChild().AddPart<GameCenter>();
            ////Step 2: 客户端初始化角色数据
            //role = Logic.AddChild().AddPart<Play>();
            //role.acceptSync = true;
            //var sceneMgr = role.entity.AddPart<SceneMgr>();

            ////Step 3: 服务端处理登录请求
            //var srole = gameCenter.Login(device.Peer1,  UtilRandom.GenID());
            ////Step 4: 服务端数据发送给客户端
            //var dyc = srole.Save();
            //role.Load(dyc);
            ////Step 5: 客户端连接设置
            //role.SetPeer(device.Peer0);

            //sceneMgr.Register<Scenes.SceneMainbase>("Main", role);
            //sceneMgr.Switch("Main");
            ////sceneMgr.Switch<Scenes.SceneMainbase>();

            //var u = gameObject.AddComponent<U>();
            //u.role = role;
            //u.srole = srole;
        }

        class U : MonoBehaviour
        {
            public Play role;
            public Play srole;

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    //UtilRandom.Select(role.mainbase.islet.buildings.FindAll(it => true)).LevelUp();
                    //srole.core.name += "@";
                    role.AskSync();
                    //UnityEngine.Debug.Break();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //UtilRandom.Select(srole.mainbase.islet.buildings.FindAll(it => true)).LevelUp();
                }
            }
        }
    }
}