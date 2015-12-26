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

namespace LogicEngine
{
    class AStar<T> where T : struct, IConvertible
    {
        TileMap<T> tileMap;
        Step[] mSteps;
        T block;

        StepOpen mOpen = new StepOpen();
        StepClose mClose = new StepClose();
        Step mBest;

        public AStar(TileMap<T> map)
        {
            tileMap = map;
            mSteps = new Step[tileMap.Size.x * tileMap.Size.y];
            UtilLambda.Foreach(tileMap.Size.x, tileMap.Size.y, (int x, int y) =>
            {
                mSteps[x + y * tileMap.Size.x] = new Step(this, x, y);
            });
            UtilLambda.Foreach(tileMap.Size.x, tileMap.Size.y, (int x, int y) =>
            {
                Step step = GetStep(new Vector2i(x, y));
                Action<int, int> fun = (int round_index_x, int round_index_y) =>
                {
                    Step round_step = GetStep(new Vector2i(round_index_x, round_index_y));
                    Node round_node = new Node();
                    round_node.Step = round_step;
                    round_node.roundType = Node.GetRoundType(round_step, step);
                    step.Nodes.Add(round_node);
                };
                ForeachRoundSteps(x, y, fun);
            });
        }
        void ForeachRoundSteps(int x, int y, Action<int, int> fun)
        {
            for (int index_x = x - 1; index_x <= x + 1; index_x++)
            {
                for (int index_y = y - 1; index_y <= y + 1; index_y++)
                {
                    if ((index_x != x || index_y != y) && tileMap.Contains(new Vector2i(index_x, index_y)))
                    {
                        fun(index_x, index_y);
                    }
                }
            }
        }
        public void SetBlock(T block)
        {
            this.block = block;
        }
        Step GetStep(Vector2i tile)
        {
            if (tileMap.Contains(tile))
            {
                return mSteps[tile.x + tile.y * tileMap.Size.x];
            }
            return null;
        }
        void Reset()
        {
            mBest = null;
            mOpen.Clear();
            mClose.Clear();
        }
        #region find
        public List<Vector2i> Find(Vector2i start, Vector2i end, bool ignore_corner)
        {
            if (Find(GetStep(start), GetStep(end), ignore_corner, 10000))
            {
                return GetPath();
            }
            return new List<Vector2i>();
        }
        List<Vector2i> GetPath()
        {
            List<Vector2i> path = new List<Vector2i>();
            int dir = 1 + (1 << 2);
            Step next = mBest;
            if (next == null)
            {
                return path;
            }
            while (true)
            {
                Step from = next;
                next = next.Parent;
                if (next != null)
                {
                    Step to = next;
                    int delta_x = to.tile.x - from.tile.x + 1;
                    int delta_y = (to.tile.y - from.tile.y + 1) << 2;
                    int delta = delta_y + delta_x;
                    //if (delta != dir)
                    {
                        path.Insert(0, from.tile);
                        dir = delta;
                    }
                }
                else
                {
                    path.Insert(0, from.tile);
                    break;
                }
            }
            return path;
        }
        bool Find(Step source, Step target, bool ignore_corner, int max_step_count = 10000)
        {
            if (source == null || target == null) return false;

            Reset();

            source.G = 0;
            source.H = Step.GetCost(source, target);
            source.F = source.G + source.H;

            Step start = source;
            mBest = source;
            start.Reset();

            int step_count = 0;
            mOpen.Push(source);
            while (mOpen.Count > 0)
            {
                ++step_count;
                Step current = mOpen.Pop();
                mClose.Push(current);

                if (current == target)
                {
                    mBest = current;
                    return true;
                }

                AddNodesToOpen(current, target, ignore_corner);

                if (step_count > max_step_count)
                {
                    return false;
                }
                if (current.H < mBest.H)
                {
                    mBest = current;
                }
            }
            return true;
        }
        void AddNodesToOpen(Step step, Step target, bool ignore_corner = true)
        {
            foreach (var node in step.Nodes)
            {
                if (node.Step.IsBlock())
                {
                    continue;
                }
                if (ignore_corner && node.roundType == RoundType.Corne)
                {
                    continue;
                }
                Step node_step = node.Step;
                if (node_step.IsClose())
                {
                    continue;
                }
                float new_g = node.Cost + step.G;

                if (node_step.IsOpen())
                {
                    if (node_step.G > new_g)
                    {
                        node_step.G = new_g;
                        node_step.F = node_step.G + node_step.H;
                        node_step.Parent = step;
                    }
                }
                else
                {
                    node_step.G = new_g;
                    node_step.H = Step.GetCost(node_step, target);
                    node_step.F = node_step.G + node_step.H;
                    node_step.Parent = step;
                    mOpen.Push(node_step);
                }
            }
        }

        bool IsBlocked(Vector2i tile)
        {
            return tileMap.HasMask(tile, block);
        }
        #endregion
        #region buffer
        enum StepState
        {
            None,
            Open,
            Close
        }
        enum RoundType
        {
            Side,
            Corne
        }
        class Step
        {
            public Step Parent;
            public List<Node> Nodes { get; private set; }

            public StepState State = StepState.None;
            public float G;
            public float H;
            public float F;//F = G + H
            public Vector2i tile;
            AStar<T> aStar;

            public Step(AStar<T> astar, int x, int y)
            {
                aStar = astar;
                Nodes = new List<Node>(8);
                tile = new Vector2i(x, y);
                Reset();
            }

            public bool IsOpen()
            {
                return (State == StepState.Open);
            }

            public bool IsClose()
            {
                return (State == StepState.Close);
            }

            public bool IsBlock()
            {
                return aStar.IsBlocked(tile);
            }

            public void Reset()
            {
                G = 0;
                Parent = null;
                State = StepState.None;
            }

            public static float GetCost(Step a, Step b)
            {
                return (((float)(a.tile.x - b.tile.x)).Square() + ((float)(a.tile.y - b.tile.y)).Square()).Sqrt();
            }
        }
        class Node
        {
            public Step Step;
            public RoundType roundType;

            public float Cost
            {
                get
                {
                    return RoundType.Corne == roundType ? 1.414f : 1f;
                }
            }

            public static RoundType GetRoundType(Step center, Step node)
            {
                var local = node.tile - center.tile;
                if (local.x.Abs() == 1 && local.y.Abs() == 1)
                {
                    return RoundType.Corne;
                }
                return RoundType.Side;
            }
        }
        class StepOpen
        {
            class Node
            {
                public Node Next;
                public Step Step;

                public Node(Step step)
                {
                    Step = step;
                }
            }

            Node mRoot = new Node(null);
            int mCount = 0;

            public int Count { get { return mCount; } }

            public Step Pop()
            {
                mCount--;
                Node first = mRoot.Next;
                mRoot.Next = first.Next;
                return first.Step;
            }

            public void Push(Step step)
            {
                mCount++;
                step.State = StepState.Open;

                if (Count == 0)
                {
                    mRoot.Next = new Node(step);
                    return;
                }

                Node current = mRoot;

                while (current.Next != null)
                {
                    if (current.Next.Step.F < step.F)
                    {
                        current = current.Next;
                    }
                    else
                    {
                        break;
                    }
                }

                Node next = current.Next;
                current.Next = new Node(step);
                current.Next.Next = next;
            }

            public void Clear()
            {
                Node current = mRoot.Next;
                while (current != null)
                {
                    current.Step.Reset();
                    current = current.Next;
                }
                mCount = 0;
                mRoot.Next = null;
            }
        }
        class StepClose
        {
            Stack<Step> mSteps = new Stack<Step>();

            public void Push(Step step)
            {
                step.State = StepState.Close;
                mSteps.Push(step);
            }
            public void Clear()
            {
                foreach (var it in mSteps)
                {
                    it.Reset();
                }
                mSteps.Clear();
            }
        }
        #endregion
    }
}