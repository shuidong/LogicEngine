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
using UnityEngine;
using UnityEngine.UI;

namespace LogicEngine.Unity.Toolkit
{
    public class UiSketch : Ui
    {
        Sketch mSketch;

        internal override void _Init0(GameObject prefab)
        {
            var go_image = new GameObject("Sketch", typeof(RectTransform));
            go_image.transform.SetParent(prefab.transform);
            var image = go_image.AddComponent<Image>();
            image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 0.3f);
            mSketch = image.gameObject.AddComponent<Sketch>();
        }
        protected override void _Init()
        {
        }

        public void SetSize(int width, int height)
        {
            mSketch.Resize(width, height);
        }
        public void Clear(Color color)
        {
            mSketch.Clear(color);
        }
        public void Apply()
        {
            mSketch.Apply();
        }

        public void DrawPoint(int x, int y, Color color)
        {
            mSketch.DrawPoint(x, y, color);
        }
        public void DrawPoint(Vector2i point, Color color)
        {
            mSketch.DrawPoint(point, color);
        }
        public void DrawPoint(Vector2f point, Color color)
        {
            mSketch.DrawPoint(point, color);
        }
        public void DrawLine(int a_x, int a_y, int b_x, int b_y, Color color)
        {
            mSketch.DrawLine(a_x, a_y, b_x, b_y, color);
        }
        public void DrawLine(Vector2i a, Vector2i b, Color color)
        {
            mSketch.DrawLine(a, b, color);
        }
        public void DrawLine(Vector2f a, Vector2f b, Color color)
        {
            mSketch.DrawLine(a, b, color);
        }
        public void DrawArray(int a_x, int a_y, int b_x, int b_y, float array_size, Color color)
        {
            mSketch.DrawArray(a_x, a_y, b_x, b_y, array_size, color);
        }
        public void DrawArray(Vector2i a, Vector2i b, float array_size, Color color)
        {
            mSketch.DrawArray(a, b, array_size, color);
        }
        public void DrawArray(Vector2f a, Vector2f b, float array_size, Color color)
        {
            mSketch.DrawArray(a, b, array_size, color);
        }
        public void DrawRect(Physics2D.Rect rect, Color color)
        {
            mSketch.DrawRect(rect, color);
        }
        public void DrawFlowField(Physics2D.FlowField field, Color color)
        {
            mSketch.DrawFlowField(field, color);
        }
        public void DrawCellular(Physics2D.Cellular1 cellular, Vector2f center, Vector2f cell_size, Color color, Color linecolorr)
        {
            mSketch.DrawCellular(cellular, center, cell_size, color, linecolorr);
        }
        public void DrawPath(Physics2D.Path path, Color color)
        {
            mSketch.DrawPath(path, color);
        }
        public void DrawString(string str, Color color)
        {
            mSketch.DrawString(str, color);
        }
    }
}