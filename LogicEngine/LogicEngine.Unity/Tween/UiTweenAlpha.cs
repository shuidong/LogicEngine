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

    public class UiTweenAlpha : UiTween
    {
        [Range(0, 1)]
        public float show = 1;
        [Range(0, 1)]
        public float hide = 0;
        CanvasRenderer[] mRenderer;

        protected override void _Init()
        {
            mRenderer = GetComponentsInChildren<CanvasRenderer>();
        }

        protected override void _OnChange(float factor, bool isFinished)
        {
            SetAlpha(UtilMath.LerpUnclamped(show, hide, factor));
        }

        void SetAlpha(float alpha)
        {
            foreach (var it in mRenderer)
            {
                it.SetAlpha(alpha);
            }
        }
    }
}