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
using LogicEngine.AI;

namespace Demos.BoomBeach
{
    public class AiSCooly : LogicEngine.AI.AiSTree<AiSCooly, Cooly>
    {
        protected override IO _InitTree()
        {
            return
                Select(
                  Sequence(spt => spt.host.IsBattle, new HideToBase(), new WalkAlongPath(), new Hide()),
                  RSelect(
                      Sequence(null, new IdleRadomDuration(), new Idle()),
                      Sequence(null, new WorkRandomVariaPath(), new WalkAlongPath(), new WorkToBase(), new WalkAlongPath(), new WorkWaitForPutWood())));
        }

        class HideToBase : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.host.GetPathToBase();
                if (path == null)
                {
                    return AiResult.Failure;
                }
                ctx.database.SetValue(KeyWalkAlongPath, new LogicEngine.AI.Path(path));
                ctx.database.SetValue<float>(KeyWalkAlongPathSpeed, 20);
                ctx.database.SetValue<float>(KeyWalkAlongPathLerp, 0);
                return AiResult.Success;
            }
        }
        class Hide : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                if (ctx.host.IsBattle)
                {
                    UtilLog.LogWarning("躲起来了");
                    return AiResult.Continue;
                }
                return AiResult.Success;
            }
        }

        static readonly string KeyIdleDuration = "KeyIdleDuration";
        class IdleRadomDuration : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                ctx.database.SetValue<float>(KeyIdleDuration, UtilRandom.Range(3, 7f));

                UtilLog.LogWarning("IdleRadomDuration");
                return AiResult.Success;
            }
        }
        class Idle : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var duration = ctx.database.GetValue<float>(KeyIdleDuration);
                if (duration > 0)
                {
                    UtilLog.LogWarning("休息一下 " + duration);
                    ctx.database.SetValue<float>(KeyIdleDuration, duration - ctx.deltaTime);
                    return AiResult.Continue;
                }
                else
                {
                    return AiResult.Success;
                }
            }
        }
        static readonly string KeyWalkAlongPath = "KeyWalkAlongPath";
        static readonly string KeyWalkAlongPathSpeed = "KeyWalkAlongPathSpeed";
        static readonly string KeyWalkAlongPathLerp = "KeyWalkAlongPathLerp";
        class WorkRandomVariaPath : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.host.GetPathToRandomVaria();
                if (path == null)
                {
                    return AiResult.Failure;
                }
                ctx.database.SetValue(KeyWalkAlongPath, new LogicEngine.AI.Path(path));
                ctx.database.SetValue<float>(KeyWalkAlongPathSpeed, 10);
                ctx.database.SetValue<float>(KeyWalkAlongPathLerp, 0);
                return AiResult.Success;
            }
        }
        class WalkAlongPath : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.database.GetValue<LogicEngine.AI.Path>(KeyWalkAlongPath);
                var lerp = ctx.database.GetValue<float>(KeyWalkAlongPathLerp);
                if (lerp > 1)
                {
                    ctx.host.SetPosition(path.Lerp(lerp));
                    lerp += ctx.database.GetValue<float>(KeyWalkAlongPathSpeed) * ctx.deltaTime / path.TotalDistance;
                    ctx.database.SetValue<float>(KeyWalkAlongPathLerp, lerp);
                    return AiResult.Continue;
                }
                return AiResult.Success;
            }
        }
        class WorkToBase : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.host.GetPathToBase();
                if (path == null)
                {
                    return AiResult.Failure;
                }
                ctx.database.SetValue(KeyWalkAlongPath, new LogicEngine.AI.Path(path));
                ctx.database.SetValue<float>(KeyWalkAlongPathSpeed, 10);
                ctx.database.SetValue<float>(KeyWalkAlongPathLerp, 0);
                return AiResult.Success;
            }
        }
        static readonly string KeyWorkWaitForPutWoodDuration = "KeyWorkWaitForPutWoodDuration";
        class WorkWaitForPutWood : IO
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var duration = ctx.database.GetValue<float>(KeyWorkWaitForPutWoodDuration);
                if (duration > 0)
                {
                    UtilLog.LogWarning("存放木头 " + duration);
                    ctx.database.SetValue<float>(KeyIdleDuration, duration - ctx.deltaTime);
                    return AiResult.Continue;
                }
                else
                {
                    return AiResult.Success;
                }
            }
        }
    }
}