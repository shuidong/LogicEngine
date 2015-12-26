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

    public class UiTweenPosition : UiTween
    {
        public Vector2 show;
        public Vector2 hide;
        RectTransform uiTransform;

        protected override void _Init()
        {
            uiTransform = transform as RectTransform;
            UtilAssert.IsNotNull(uiTransform, name);
        }

        protected override void _OnChange(float factor, bool isFinished)
        {
            uiTransform.anchoredPosition = show * (1 - factor) + hide * factor;
        }
    }
}