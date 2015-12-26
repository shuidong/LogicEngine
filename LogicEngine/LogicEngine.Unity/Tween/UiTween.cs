//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;

namespace LogicEngine.Unity
{

    public static class RealTime
    {
        public static float time { get { return Time.time; } }
        public static float deltaTime { get { return Time.deltaTime; } }
    }

    [RequireComponent(typeof(RectTransform))]
    public abstract class UiTween : Tween
    {
        public ToggleStyle toggleStyle = ToggleStyle.ShowOrHide;

        protected override void Awake()
        {
            base.Awake();
            if (toggleStyle == ToggleStyle.Auto)
            {
                enabled = true;
                PalyForward();
            }
        }
    }
}