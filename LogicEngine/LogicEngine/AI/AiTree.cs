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
    public enum AiResult
    {
        Continue,
        Success,
        Failure
    }

    public class Database
    {
        Dictionary<string, object> mObjs = new Dictionary<string, object>();

        public T GetValue<T>(string key)
        {
            object svalue = null;
            if (mObjs.TryGetValue(key, out svalue))
            {
                return (T)svalue;
            }
            return default(T);
        }
        public void SetValue<T>(string key, T value)
        {
            //UtilLog.LogWarning(value.ToString());
            mObjs[key] = value;
        }
    }

    public class Path
    {
        List<Vector2f> mPoints = new List<Vector2f>();
        List<float> mDistances = new List<float>();
        public float TotalDistance { get; private set; }

        public Path(List<Vector2f> points)
        {
            float dis = 0;
            Vector2f p0 = points[0];
            mPoints.Add(points[0]);
            mDistances.Add(dis);

            for (int i=1;i<points.Count;i++)
            {
                dis += Vector2f.Distance(p0, points[i]);
                mPoints.Add(points[i]);
                mDistances.Add(dis);
                p0 = points[1];
            }
            TotalDistance = mDistances[mDistances.Count - 1];
        }

        public Vector2f Lerp(float t)
        {
            var need_distance = TotalDistance * t;
            for (int i = 0; i < mPoints.Count; i++)
            {
                if (need_distance < mDistances[i])
                {
                    return Vector2f.Lerp(mPoints[i-1], mPoints[i], (need_distance - mDistances[i - 1])/ (mDistances[i] - mDistances[i-1])); ;
                }
            }
            return mPoints[mPoints.Count - 1];
        }

        public override string ToString()
        {
            return UtilString.Join('-', mPoints.ToArray());
        }
    }

    public abstract class AiTree<TTree, THost>
        where TTree : AiTree<TTree, THost>, new()
        where THost : Part, new()
    {
        static TTree singleton;
        internal static TTree Instance
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
        List<IO> mAborts = new List<IO>();

        public class Spirit : UPart
        {
            public THost host { get; private set; }
            public Database database { get; private set; }
            public float deltaTime { get; private set; }
            internal IO Current { get; set; }
            public Continuation Continuation { get; set; }

            internal override void _Awake0()
            {
                base._Awake0();
                Continuation = new Continuation();
                Current = Instance.mRoot;
                host = GetPart<THost>();
                database = new Database();
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
                Instance.mRoot.Drive(this);
            }

            internal bool IsSibTo(IO io)
            {
                var cur = Current;
                while (cur != null)
                {
                    if (cur == io)
                    {
                        return true;
                    }
                    cur = cur.Parent;
                }
                return false;
            }
        }

        protected static IO Parallel(params IO[] ios)
        {
            return null;
        }
        protected static IO Check(Func<Spirit, bool> fun, IO io)
        {
            return new IOCheck(fun, io);
        }
        protected static IO Sequence(params Node[] ios)
        {
            return new IOSequence(ios);
        }
        protected static IO Abort(Func<Spirit, bool> fun, IO io)
        {
            var abort = new IOAbort(fun, io);
            Instance.mAborts.Add(abort);
            return abort;
        }

        class IOAbort : IO
        {
            Func<Spirit, bool> mCheck;
            IO mIO;

            public IOAbort(Func<Spirit, bool> fun, IO io)
            {
                mCheck = fun;
                mIO = io;
            }

            internal override AiResult Drive(Spirit ctx)
            {
                if (mCheck(ctx))
                {
                    var result = mIO.Drive(ctx);
                    return result;
                }
                return AiResult.Failure;
            }
        }
        class IOCheck : IO
        {
            Func<Spirit, bool> mCheck;
            IO mIO;

            public IOCheck(Func<Spirit, bool> fun, IO io)
            {
                mCheck = fun;
                mIO = io;
            }

            internal override AiResult Drive(Spirit ctx)
            {
                if (mCheck(ctx))
                {
                    var result = mIO.Drive(ctx);
                    return result;
                }
                return AiResult.Failure;
            }
        }
        class IOSequence : IO
        {
            List<IO> mSequence = new List<IO>();

            public IOSequence(IO[] ios)
            {
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
            internal override AiResult Drive(Spirit ctx)
            {
                if (ctx.IsSibTo(this))
                {
                    if (ctx.Current == this)
                    {
                        ctx.Current = mSequence[0];
                    }
                    var child = FindSibTo(ctx);
                    return DriveChild(ctx, child);
                }
                return AiResult.Failure;
            }
            AiResult DriveChild(Spirit ctx, IO child)
            {
                var result = child.Drive(ctx);
                switch (result)
                {
                    case AiResult.Continue:
                        return AiResult.Continue;
                    case AiResult.Failure:
                        break;
                    case AiResult.Success:
                        if (ctx.Current.Right == null)
                        {
                            ctx.Current = this;
                            return AiResult.Success;
                        }
                        else
                        {
                            ctx.Current = ctx.Current.Right;
                            return DriveChild(ctx, ctx.Current);
                        }
                }
                return AiResult.Failure;
            }
            IO FindSibTo(Spirit ctx)
            {
                foreach (var it in mSequence)
                {
                    if (ctx.IsSibTo(it))
                    {
                        return it;
                    }
                }
                return null;
            }
        }

        public abstract class Thunk<T>
        {
            public abstract T GetUserValue();
        }

        public abstract class IO
        {
            internal IO Parent;
            //internal IO List<IO> Children = new;
            internal IO Left;
            internal IO Right;

            internal abstract AiResult Drive(Spirit ctx);

            public string ToTree()
            {
                return base.ToString();
            }
            //static string ToTree(int gap_count, IO entity)
            //{
            //    string s = Pattern(gap_count, "----", "--->") + "[" + GetParts(entity) + "]\n";
            //    foreach (var it in entity.mChildren)
            //    {
            //        s += ToTree(gap_count + 1, it);
            //    }
            //    return s;
            //}
            //static string GetParts(Entity entity)
            //{
            //    StringBuilder result = new StringBuilder();
            //    int index = 0;
            //    foreach (var it in entity.mParts)
            //    {
            //        result.Append(it.Key.Name + (index == entity.mParts.Count - 1 ? "" : ","));
            //        index++;
            //    }
            //    return result.ToString();
            //}
            //static string Pattern(int count, params string[] patterns)
            //{
            //    if (patterns.Length == 0)
            //    {
            //        throw new Exception("must have pattern");
            //    }
            //    StringBuilder result = new StringBuilder();
            //    int diff = count - patterns.Length;
            //    for (int i = 0; i < count; ++i)
            //    {
            //        int index = i - diff;
            //        if (index < 0) index = 0;
            //        result.Append(patterns[index]);
            //    }
            //    return result.ToString();
            //}
        }
        public abstract class Node : IO
        {
            internal override AiResult Drive(Spirit ctx)
            {
                if (ctx.Current == this)
                {
                    var result = _Drive(ctx);
                    switch (result)
                    {
                        case AiResult.Continue:
                            return AiResult.Continue;
                        case AiResult.Failure:
                            return AiResult.Failure;
                        case AiResult.Success:
                            return AiResult.Success;
                    }
                }
                return AiResult.Failure;
            }
            protected abstract AiResult _Drive(Spirit ctx);

            //public override IO GetUserValue()
            //{
            //    return this;
            //}
        }
        public class Continuation
        {
            public Continuation SubContinuation { get; set; }
            public int NextStep { get; set; }
            public object Param { get; set; }
        }
    }
}