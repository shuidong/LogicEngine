//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System.Collections.Generic;

namespace LogicEngine
{
    public class Fsm<T> where T : class
    {
        T Holder { get; set; }
        State mCurrent;
        List<State> mStateQueue = new List<State>();
        LinkedList<State> mEnterStateQueue = new LinkedList<State>();
        Dictionary<string, State> mStates = new Dictionary<string, State>();

        public Fsm(T holder)
        {
            Holder = holder;
        }
        public bool IsState<S>() where S : State, new()
        {
            return mStateQueue.Count == 0 ? mCurrent is S : mStateQueue[mStateQueue.Count - 1] is S;
        }
        public void Release()
        {
            if (mCurrent != null)
            {
                mCurrent.Exit();
                mCurrent = null;
            }
            mStateQueue.Clear();
            mEnterStateQueue.Clear();
        }
        public void Switch<S>() where S : State, new()
        {
            string key = typeof(S).Name;
            State state = null;
            if (!mStates.TryGetValue(key, out state))
            {
                state = CreateState<S>();
                mStates.Add(key, state);
            }

            bool n = mStateQueue.Count == 0 && mEnterStateQueue.Count == 0;
            mStateQueue.Add(state);

            if (n)
            {
                for (int i = 0; i < mStateQueue.Count; i++)
                {
                    mEnterStateQueue.AddLast(mStateQueue[i]);
                }
                mStateQueue.Clear();

                while (mEnterStateQueue.Count > 0)
                {
                    var next = mEnterStateQueue.First;
                    if (mCurrent != null)
                    {
                        mCurrent.Exit();
                    }
                    mCurrent = next.Value;
                    mCurrent.Enter();

                    mEnterStateQueue.RemoveFirst();
                    for (int i = mStateQueue.Count - 1; i >= 0; i--)
                    {
                        mEnterStateQueue.AddFirst(mStateQueue[i]);
                    }
                    mStateQueue.Clear();
                }
            }
        }

        public void Update(float elapsed_sec)
        {
            if (mCurrent == null) return;
            mCurrent.Update(elapsed_sec);
        }
        void Sync()
        {
            foreach (var it in mStateQueue)
            {
                mEnterStateQueue.AddFirst(it);
            }
            mStateQueue.Clear();
        }
        State CreateState<S>() where S : State, new()
        {
            S state = new S();
            state.Init(this, Holder);
            return state;
        }

        public abstract class State
        {
            Fsm<T> fsm { get; set; }
            protected T Holder { get; private set; }

            internal void Init(Fsm<T> machine, T holder)
            {
                fsm = machine;
                Holder = holder;
                _Init();
            }

            internal void Enter()
            {
                _Enter();
            }

            internal void Update(float elapsed_sec)
            {
                _Update(elapsed_sec);
            }

            internal void Exit()
            {
                _Exit();
            }

            protected virtual void _Init() { }
            protected abstract void _Enter();
            protected abstract void _Update(float elapsed_sec);
            protected abstract void _Exit();

            protected void Switch<S>() where S : State, new()
            {
                fsm.Switch<S>();
            }
        }
    }
}
