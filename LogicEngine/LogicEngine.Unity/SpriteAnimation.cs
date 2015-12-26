//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LogicEngine.Unity
{
    public class SpriteAnimation : MonoBehaviour
    {
        public Sprite[] sprites;
        public float frameRate;
        SpriteRenderer renderer;
        int current;

        void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            //renderer.sprite = sprites[current];
        }

        void Update()
        {
            if (sprites == null || sprites.Length == 0) return;

            var index = (int)(Time.timeSinceLevelLoad * frameRate) % sprites.Length;
            if (current != index)
            {
                current = index;
                renderer.sprite = sprites[current];
            }
        }
    }
}