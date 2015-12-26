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

    [AddComponentMenu("LogicEngine/Tween/Sprite Tween Position")]
    public class TweenPosition : Tween
    {
        public Vector3 from;
        public Vector3 to;

        protected override void _OnChange(float factor, bool isFinished)
        {
            transform.position = from * (1 - factor) + to * factor;
        }
    }
}