////==============================================================================
//// Copyright (C) 2015 Peng Guang Hui
//// All rights reserved
////
//// Create by 彭光辉 at 2015/10/16 20:30:00
//// Email: gh.peng@qq.com
////==============================================================================
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using LogicEngine;

//namespace Demos.BoomBeach.Server
//{
//    public class GuildCenter : Center<GuildCenter>
//    {
//        public EntityMgr<Guild, DycGuild> guilds { get; private set; }

//        protected override void _Awake()
//        {
//            guilds = AddPart<EntityMgr<Guild, DycGuild>>();

//            AddRequest<Requests.Guild.CreateGuild>(CreateGuild);
//        }
//        protected override void _Release()
//        {
//        }
//        void CreateGuild(Requests.Guild.CreateGuild request)
//        {
//            if (guilds.Find(it => it.Name == request.Name) == null)
//            {
//            }
//            else
//            {
//            }
//        }

//        public enum RpcMessage
//        {
//            ReqCreateGuild,
//        }
//    }
//}