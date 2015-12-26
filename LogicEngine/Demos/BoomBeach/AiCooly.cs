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
    public class AiCooly : LogicEngine.AI.AiTree<AiCooly, Cooly>
    {
        protected override IO _InitTree()
        {
            return
                Sequence(new RadomVaria(), new MoveToVaria(), new SelectBase(), new MoveToVaria(), new WaitForPutWood());
        }

        static readonly string KeyMovePath = "KeyMovePath";
        static readonly string KeyMovePathLerp = "KeyMovePathLerp";

        public class RadomVaria : Node
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.host.GetPathToRandomVaria();
                if(path == null)
                {
                    return AiResult.Failure;
                }
                ctx.database.SetValue(KeyMovePath, new LogicEngine.AI.Path(path));
                ctx.database.SetValue<float>(KeyMovePathLerp, 0);
                return AiResult.Success;
            }
        }
        public class MoveToVaria : AiCooly.Node
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.database.GetValue<LogicEngine.AI.Path>(KeyMovePath);
                var lerp = ctx.database.GetValue<float>(KeyMovePathLerp);
                lerp += 10f * ctx.deltaTime;
                ctx.database.SetValue<float>(KeyMovePathLerp, lerp);

                ctx.host.SetPosition(path.Lerp(lerp / path.TotalDistance));
                if (lerp / path.TotalDistance >= 1)
                {
                    UtilLog.LogWarning("OK");
                }
                return lerp / path.TotalDistance >= 1 ? AiResult.Success : AiResult.Continue;
            }
        }
        public class SelectBase : Node
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var path = ctx.host.GetPathToBase();
                if (path == null)
                {
                    return AiResult.Failure;
                }
                UtilLog.LogWarning("OK");
                ctx.database.SetValue("Path", new LogicEngine.AI.Path(path));
                ctx.database.SetValue<float>("Path.Lerp", 0);
                ctx.database.SetValue<float>("WaitForPutWood", 5);
                return AiResult.Success;
            }
        }
        public class WaitForPutWood : Node
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                var lerp = ctx.database.GetValue<float>("WaitForPutWood");
                lerp -= ctx.deltaTime;
                ctx.database.SetValue<float>("WaitForPutWood", lerp);
                if (lerp <= 0)
                {
                    UtilLog.LogWarning("OK");
                }
                return lerp <= 0 ? AiResult.Success : AiResult.Continue;
            }
        }


            public class IdleState : AiCooly.Node
        {
            protected override AiResult _Drive(Spirit ctx)
            {
                return AiResult.Continue;
            }
        }

        public class PatrolState : AiCooly.Node
        {
            private readonly List<AiCooly.Node> subStates = new List<Node>();
            public PatrolState()
            {
                //subStates.Add(new MoveToState());
                subStates.Add(new IdleState());
            }
            protected override AiResult _Drive(Spirit ctx)
            {
                return AiResult.Continue;
            }
        }
    }
}