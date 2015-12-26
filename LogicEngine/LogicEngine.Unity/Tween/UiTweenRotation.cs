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

    public class UiTweenRotation : UiTween
    {
        public Vector3 show;
        public Vector3 hide;
        RectTransform uiTransform;

        protected override void _Init()
        {
            uiTransform = GetComponent<RectTransform>();
        }

        protected override void _OnChange(float factor, bool isFinished)
        {
            uiTransform.rotation = Quaternion.Euler(new Vector3(
                UtilMath.LerpUnclamped(show.x, hide.x, factor),
                UtilMath.LerpUnclamped(show.y, hide.y, factor),
                UtilMath.LerpUnclamped(show.z, hide.z, factor)));
        }
    }
}