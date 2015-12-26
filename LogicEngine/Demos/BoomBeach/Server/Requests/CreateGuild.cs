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
using System.Text.RegularExpressions;
using LogicEngine;

namespace Demos.BoomBeach.Server.Requests.Guild
{
    public class CreateGuild : Request<CreateGuild.Response>
    {
        public long ID;
        public string Name;

        protected override bool _TryVerify(ref string error)
        {
            if (string.IsNullOrEmpty(Name))
            {
                error = "请求的公会名不能为空";
                return false;
            }
            return true;
        }

        static Regex d = new Regex(@"[\u4e00-\u9fbb]+{1}", RegexOptions.Compiled);

        public class Response : Request.Response
        {
        }
    }
}