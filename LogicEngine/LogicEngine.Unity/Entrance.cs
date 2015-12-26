//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicEngine.Unity
{
    public abstract class Entrance : MonoSingleton<Entrance>
    {
        public bool enableDebug
        {
            get { return mDebug.enableDebug; }
            set { mDebug.enableDebug = value; }
        }
        MonoDebug mDebug;

        internal override void Awake()
        {
            mDebug = gameObject.AddComponent<MonoDebug>();
            InitUnity();
            InitEngine();
            base.Awake();
        }
        void InitUnity()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            Application.runInBackground = true;
            Application.targetFrameRate = 50;
            Application.backgroundLoadingPriority = ThreadPriority.Low;

            QualitySettings.vSyncCount = 0;

            Input.simulateMouseWithTouches = true;
        }
        void InitEngine()
        {
            Logic.Init(new LogicAdapter());
        }

        void Update()
        {
            Logic.Update(Time.deltaTime);
        }

        void OnApplicationFocus(bool focusStatus)
        {
            //Debug.LogWarning("OnApplicationFocus");
        }
        void OnApplicationPause(bool pauseStatus)
        {
            //Debug.LogWarning("OnApplicationPause");
        }
        void OnApplicationQuit()
        {
            isRunning = false;
            Logic.Release();
            Debug.LogWarning("OnApplicationQuit");
        }
        internal static bool isRunning = true;

        public void AddDebugBtn(string name, Action fun)
        {
            mDebug.AddDebugBtn(name, fun);
        }
        public void AddDebugLabel(string name, Func<string> fun)
        {
            mDebug.AddDebugLabel(name, fun);
        }
    }
}