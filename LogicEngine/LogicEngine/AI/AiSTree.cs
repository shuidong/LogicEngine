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

namespace LogicEngine.AI
{
    public abstract class AiSTree<TTree, THost>
            where TTree : AiSTree<TTree, THost>, new()
            where THost : Part, new()
    {
        static TTree singleton;
        static TTree Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new TTree();
                    singleton.mRoot = singleton._InitTree();
                }
                return singleton;
            }
        }
        protected abstract IO _InitTree();

        IO mRoot;

        public class Spirit : UPart
        {
            public THost host { get; private set; }
            public Database database { get; private set; }
            public float deltaTime { get; private set; }
            internal IO Current { get; set; }

            internal override void _Awake0()
            {
                base._Awake0();
                host = GetPart<THost>();
                database = new Database();
                Current = Instance.mRoot;
            }

            protected override void _Awake()
            {
            }
            protected override void _Release()
            {
            }
            protected override void _Update(float sec)
            {
                deltaTime = sec;
                Instance.mRoot.Evluate(this);
                Current.Drive(this);
            }
            internal bool IsChildOf(IO parent)
            {
                if (Current == null)
                {
                    return false;
                }
                else
                {
                    var nonius = Current.Parent;
                    while (nonius != null)
                    {
                        if (nonius == parent)
                        {
                            return true;
                        }
                        nonius = nonius.Parent;
                    }
                    return false;
                }
            }
        }

        protected static IO Sequence(Func<Spirit, bool> precondition, params IO[] ios)
        {
            return new IOSequence(precondition, ios);
        }
        protected static IO Select(params IO[] ios)
        {
            return new IOSelection(IOSelection.Mode.Priority, ios);
        }
        protected static IO RSelect(params IO[] ios)
        {
            return new IOSelection(IOSelection.Mode.Random, ios);
        }

        internal class IOSelection : IO
        {
            List<IO> mSelection = new List<IO>();
            Mode mMode;

            public IOSelection(Mode mode, IO[] ios)
            {
                mMode = mode;
                for (int i = 0; i < ios.Length; i++)
                {
                    ios[i].Parent = this;
                    if (i > 0)
                    {
                        ios[i].Left = ios[i - 1];
                    }
                    if (i < ios.Length - 1)
                    {
                        ios[i].Right = ios[i + 1];
                    }
                }
                mSelection.AddRange(ios);
            }
            internal override bool Evluate(Spirit ctx)
            {
                if (ctx.IsChildOf(this))
                {
                    return true;
                }
                switch (mMode)
                {
                    case Mode.Priority:
                        foreach (var it in mSelection)
                        {
                            if (it is IOSelection)
                            {
                                if (it.Evluate(ctx))
                                {
                                    return true;
                                }
                            }
                            else if (it is IOSequence)
                            {
                                if (it.Evluate(ctx))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return it.Evluate(ctx);
                            }
                        }
                        break;
                    case Mode.Random:
                        List<IO> list = new List<IO>();
                        list.AddRange(mSelection);
                        while (list.Count > 0)
                        {
                            UtilLog.LogWarning("212---c--->" + mSelection.Count);
                            var it = UtilRandom.Select(list);
                            list.Remove(it);
                            if (it.Evluate(ctx))
                            {
                                return true;
                            }
                        }
                        break;
                }
                return false;
            }
            protected override AiResult _Drive(Spirit ctx)
            {
                throw new NotImplementedException();
            }

            internal override void RaiseUp(Spirit spt, AiResult result)
            {
                spt.Current = this;
                if (Parent != null)
                {
                    Parent.RaiseUp(spt, AiResult.Success);
                }
            }
            public enum Mode
            {
                Priority,
                Random
            }
        }
        internal class IOSequence : IO
        {
            Func<Spirit, bool> mPrecondition;
            List<IO> mSequence = new List<IO>();

            public IOSequence(Func<Spirit, bool> precondition, IO[] ios)
            {
                mPrecondition = precondition;
                for (int i = 0; i < ios.Length; i++)
                {
                    ios[i].Parent = this;
                    if (i > 0)
                    {
                        ios[i].Left = ios[i - 1];
                    }
                    if (i < ios.Length - 1)
                    {
                        ios[i].Right = ios[i + 1];
                    }
                }
                mSequence.AddRange(ios);
            }
            internal override bool Evluate(Spirit ctx)
            {
                if (ctx.IsChildOf(this))
                {
                    return true;
                }
                if (mPrecondition != null && !mPrecondition(ctx))
                {
                    return false;
                }
                if (mSequence[0].Evluate(ctx))
                {
                    return true;
                }
                return false;
            }
            internal override void Drive(Spirit ctx)
            {
                if (ctx.Current == this)
                {
                    ctx.Current = mSequence[0];
                    //return ctx.Current.Drive(ctx);
                }
                //return AiResult.Failure;
            }
            protected override AiResult _Drive(Spirit ctx)
            {
                throw new NotImplementedException();
            }

            internal override void RaiseUp(Spirit spt, AiResult result)
            {
                if (spt.Current.Right == null)
                {
                    spt.Current = this;
                    if (Parent != null)
                    {
                        Parent.RaiseUp(spt, AiResult.Success);
                    }
                }
                else
                {
                    spt.Current = spt.Current.Right;
                }
            }
        }
        public abstract class IO
        {
            internal IO Parent;
            internal IO Left;
            internal IO Right;

            internal virtual bool Evluate(Spirit ctx)
            {
                ctx.Current = this;
                UtilLog.LogWarning(ctx.Current.GetType().Name);
                return true;
            }
            internal virtual void Drive(Spirit ctx)
            {
                var result = _Drive(ctx);
                switch (result)
                {
                    case AiResult.Continue:
                        break;
                    case AiResult.Failure:
                        Parent.RaiseUp(ctx, AiResult.Failure);
                        break;
                    case AiResult.Success:
                        Parent.RaiseUp(ctx, AiResult.Success);
                        break;
                }
            }
            internal virtual void RaiseUp(Spirit spt, AiResult result) { }
            protected abstract AiResult _Drive(Spirit ctx);
        }
    }
}