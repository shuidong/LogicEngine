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

namespace Demos.BoomBeach.Server
{
    public class ChatCenter : Center<ChatCenter>
    {
        protected override void _Awake()
        {
            AddRequest<ChatToWorld>(OnChatToWorld);
        }
        protected override void _Release()
        {
        }
        void OnChatToWorld(ChatToWorld request)
        {
            UtilLog.LogWarning(request.d0);
        }
    }

    public class ChatUser : ChatCenter.User
    {
        protected override void _Awake()
        {
        }
        protected override void _Release()
        {
        }
        public void ChatWorld(string msg)
        {
        }
    }

    public class ChatToWorld : Request<ChatToWorld.Response>
    {
        /// <summary>
        /// msg
        /// </summary>
        public string d0;

        protected override bool _TryVerify(ref string error)
        {
            if (string.IsNullOrEmpty(d0))
            {
                error = "聊天语句为空";
                return false;
            }
            return true;
        }

        public class Response : Request.Response
        {
        }
    }
}