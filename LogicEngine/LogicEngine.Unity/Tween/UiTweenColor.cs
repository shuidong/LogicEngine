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

    public class UiTweenColor : UiTween
    {
        public Color show = Color.white;
        public Color hide = Color.white;
        CanvasRenderer[] mRenderer;

        protected override void _Init()
        {
            mRenderer = GetComponentsInChildren<CanvasRenderer>();
        }

        protected override void _OnChange(float factor, bool isFinished)
        {
            SetColor(Color.Lerp(show, hide, factor));
        }

        void SetColor(Color color)
        {
            foreach (var it in mRenderer)
            {
                it.SetColor(color);
            }
        }
    }
}