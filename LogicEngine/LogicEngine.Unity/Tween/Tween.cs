//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using UnityEngine;

namespace LogicEngine.Unity
{

    public abstract class Tween : MonoBehaviour
    {
        public WrapMode wrapMode = WrapMode.Once;
        public bool customCurve;
        public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        public TweenStyle tweenStyle = TweenStyle.Linear;
        public bool ignoreTimeScale = true;
        [Range(0, float.MaxValue)]
        public float delay = 0f;
        [Range(0, float.MaxValue)]
        public float duration = 1f;

        public Action OnFinished;

        bool mIsPlaying;
        float mStartTime = 0f;
        bool mForword = true;
        float mFactor;
        float mFactorRate;

        public void PalyForward()
        {
            Play(true);
        }
        public void PalyBackward()
        {
            Play(false);
        }

        public void Reset(bool forward)
        {
            mFactor = forward ? 0f : 1f;
            Sample(mFactor, false);
            mIsPlaying = false;
            enabled = false;
        }

        void Play(bool forward)
        {
            if (mIsPlaying && mForword == forward) return;
            enabled = true;
            mIsPlaying = true;
            mFactorRate = duration > 0 ? 1f / duration : 1000f;
            mForword = forward;
            mStartTime = (ignoreTimeScale ? RealTime.time : Time.time) + delay;
        }

        protected virtual void Awake()
        {
            _Init();
            enabled = false;
        }

        void Update()
        {
            float delta_time = ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime;
            float time = ignoreTimeScale ? RealTime.time : Time.time;

            if (time < mStartTime) return;

            switch (wrapMode)
            {
                case WrapMode.Once:
                    if (mForword)
                    {
                        mFactor = mFactor + mFactorRate * delta_time;
                        if (mFactor >= 1f)
                        {
                            mFactor = 1f;
                            Sample(mFactor, true);
                        }
                        else
                        {
                            Sample(mFactor, false);
                        }
                    }
                    else
                    {
                        mFactor = mFactor - mFactorRate * delta_time;
                        if (mFactor <= 0f)
                        {
                            mFactor = 0f;
                            Sample(mFactor, true);
                        }
                        else
                        {
                            Sample(mFactor, false);
                        }
                    }
                    break;
                case WrapMode.PingPong:
                    if (mForword)
                    {
                        mFactor = mFactor + mFactorRate * delta_time;
                        if (mFactor >= 1f)
                        {
                            mFactor = 1f;
                            mForword = false;
                        }
                        Sample(mFactor, false);
                    }
                    else
                    {
                        mFactor = mFactor - mFactorRate * delta_time;
                        if (mFactor <= 0f)
                        {
                            mFactor = 0f;
                            mForword = true;
                            Sample(mFactor, true);
                        }
                        else
                        {
                            Sample(mFactor, false);
                        }
                    }
                    break;
                case WrapMode.Loop:
                    if (mForword)
                    {
                        mFactor = mFactor + mFactorRate * delta_time;
                        if (mFactor >= 1f)
                        {
                            mFactor = 1f;
                            mForword = false;
                        }
                        Sample(mFactor, false);
                    }
                    else
                    {
                        mFactor = mFactor - mFactorRate * delta_time;
                        if (mFactor <= 0f)
                        {
                            mFactor = 0f;
                            mForword = false;
                        }
                        Sample(mFactor, false);
                    }
                    break;
            }
        }

        void Sample(float factor, bool isFinished)
        {
            if (customCurve && animationCurve != null)
            {
                _OnChange(animationCurve.Evaluate(factor), isFinished);
            }
            else
            {
                _OnChange(UtilTween.Evaluate(tweenStyle, factor, 0f, 1f, 1f), isFinished);
            }
            if (isFinished && OnFinished != null)
            {
                mIsPlaying = false;
                OnFinished();
            }
        }

        void OnDisable() { mIsPlaying = false; }
        //void OnEnable()
        //{
        //    Reset(true);
        //    Play(true);
        //}

        protected virtual void _Init() { }
        protected abstract void _OnChange(float factor, bool isFinished);

        /// <summary>
        /// 循环模式
        /// </summary>
        public enum WrapMode
        {
            Once,
            Loop,
            PingPong,
        }
    }
}