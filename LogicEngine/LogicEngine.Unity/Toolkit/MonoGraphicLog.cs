//======================================================
// Create by @Peng Guang Hui
// 2015/12/8 14:48:15
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicEngine.Unity
{
    class MonoGraphicLog : Refer
    {
        protected override void _Awake()
        {
        }
    }

    class UiGraphicLog : Ui
    {
        internal override void _Init0(GameObject prefab)
        {
            var go_image = new GameObject("Image", typeof(RectTransform));
            go_image.transform.SetParent(prefab.transform);
            var image = go_image.AddComponent<Image>();
            image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 0.3f);
            mSketchpad = image.gameObject.AddComponent<Toolkit.Sketch>();
            mBgColor = new Color(0, 0, 0, 0);
        }

        Queue<float> mValues = new Queue<float>();
        int mMaxLength = 300;
        Color mBgColor;
        Toolkit. Sketch mSketchpad;

        protected override void _Init()
        {
            Logic.AddUpdate(Update);
        }

        public void LogValue(string name, float value)
        {
            Queue<float> queue = mValues;
            queue.Enqueue(value);
            if (queue.Count > mMaxLength)
            {
                queue.Dequeue();
            }
        }

        void Update(float secs)
        {
            float l = mValues.Count;
            if (l == 0) return;

            mSketchpad.Clear(mBgColor);

            float min;
            float max;
            GetMinMax(out min, out max);
            int i = 0;
            //ox = mValues[];

            float x_start = 1 - mValues.Count / (float)mMaxLength;
            ox = (int)(mSketchpad.width * x_start);
            oy = (int)(mSketchpad.height * (mValues.Peek() - min) / (max - min));
            foreach (var it in mValues)
            {
                DrawGraphPoint(x_start + i / (float)mMaxLength, it, min, max, Color.cyan);
                i++;
            }
            mSketchpad.Apply();
        }
        public void DrawGraphPoint(float x, float y, float y_min, float y_max, Color color)
        {
            int nx = (int)(mSketchpad.width * x);
            int ny = (int)(mSketchpad.height * (y - y_min) / (y_max - y_min));
            mSketchpad.DrawLine(ox, oy, nx, ny, color);
            ox = nx;
            oy = ny;
        }
        int ox;
        int oy;
        public void GetMinMax(out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;
            foreach (var it in mValues)
            {
                if (it < min)
                {
                    min = it;
                }
                if (it > max)
                {
                    max = it;
                }
            }
            //if (max - min < 200)
            //{
            //    max = min + 210;
            //}
        }
    }
}