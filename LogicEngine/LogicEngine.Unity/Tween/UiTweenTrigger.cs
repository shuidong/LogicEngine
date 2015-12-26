//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using UnityEngine;
using UnityEngine.EventSystems;

namespace LogicEngine.Unity
{

    public class UiTweenTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UiTween[] tweens;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            for (int i = 0; i < tweens.Length; ++i)
            {
                UiTween tween = tweens[i];
                if (tween != null)
                {
                    tween.Reset(false);
                    tween.Reset(false);
                }
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            for (int i = 0; i < tweens.Length; ++i)
            {
                UiTween tween = tweens[i];
                if (tween != null)
                {
                    tween.Reset(true);
                    tween.Reset(true);
                }
            }
        }
    }
}