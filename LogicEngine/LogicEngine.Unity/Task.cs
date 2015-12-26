//======================================================
// Create by @Peng Guang Hui
// 2015/11/19 16:03:30
//======================================================
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicEngine.Unity
{
    public class SceneLoader : Logic.ISceneLoader
    {
        TaskRunner mRunner = new TaskRunner();
        void Logic.ISceneLoader.Run(IEnumerator coroutine)
        {
            mRunner.Run(coroutine);
        }
        void Logic.ISceneLoader.Stop()
        {
            mRunner.Stop();
        }
    }

    public class TaskRunner
    {
        Task mTask;

        public void Run(IEnumerator coroutine, Action finish = null)
        {
            Stop();
            mTask = new Task(coroutine, finish);
            mTask.Run();
        }

        public void Stop()
        {
            if (mTask != null)
            {
                mTask.Stop();
                mTask = null;
            }
        }
    }

    public class Task
    {
        bool mInCircle;
        bool mPaused;
        Stack<IEnumerator> mStandbyers = new Stack<IEnumerator>();
        IEnumerator mRunner;

        Action mFinish;

        public Task(IEnumerator coroutine, Action finish = null)
        {
            mStandbyers.Push(coroutine);
            mFinish = finish;

            mInCircle = true;
            mPaused = true;
            TaskRunner.Instance.Run(Circle());
        }

        public void Run()
        {
            mPaused = false;
        }

        public void Pause()
        {
            mPaused = true;
        }

        public void Stop()
        {
            mInCircle = false;
        }

        public bool IsRunning()
        {
            return mInCircle && !mPaused;
        }

        public bool IsPause()
        {
            return mInCircle && mPaused;
        }

        public bool IsStop()
        {
            return !mInCircle;
        }

        IEnumerator Circle()
        {
            while (mInCircle)
            {
                if (mPaused)
                    yield return null;
                else
                {
                    if (mRunner == null)
                    {
                        if (mStandbyers.Count > 0)
                        {
                            mRunner = mStandbyers.Pop();
                        }
                        else
                        {
                            mInCircle = false;
                            break;
                        }
                    }

                    if (mRunner != null && mRunner.MoveNext())
                    {
                        IEnumerator subtask = mRunner.Current as IEnumerator;
                        if (subtask == null)
                        {
                            yield return mRunner.Current;
                        }
                        else
                        {
                            mStandbyers.Push(mRunner);
                            mRunner = subtask;
                        }
                    }
                    else
                    {
                        mRunner = null;
                    }
                }
            }
            if (mFinish != null)
                mFinish();
        }

        class TaskRunner : MonoSingleton<TaskRunner>
        {
            protected override void _Init()
            {
            }
            public void Run(IEnumerator coroutine)
            {
                StartCoroutine(coroutine);
            }
        }
    }
}